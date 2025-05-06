
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
public enum CardTargetType{Default,BuffOnly}
public class Card : CollectibleItem
{
    //public Zone GetZone();
    public Player player;
    public Card cardTemplate;
    int originalCost = -1;

    public void SetOriginalCost(int value)
    {
        originalCost = value;
    }
    public int initialCost;
public DialogueInBattleInfo dialogueInBattleInfo;
    public int level;

    [HideInInspector]
    public int CardUniqueId; //only used to mark selected items

    public Card FirstUpgradeCard => upgradeCards.Count == 0 ? null : upgradeCards[0];
    public Card RandomUpgradeCard => upgradeCards.Count == 0?null:upgradeCards.RandomItem();
    public List<Card> upgradeCards
    {
        get
        {
            List<Card> res = new List<Card>();
            if (!MenuControl.Instance.csvLoader.cardUniqueIdToUpgradeCardUniqueId.ContainsKey(this.UniqueID))
            {
                return res;
            }

            var cardIds = MenuControl.Instance.csvLoader.cardUniqueIdToUpgradeCardUniqueId[this.UniqueID];
            foreach (var cardId in cardIds)
            {
                
                if (!MenuControl.Instance.heroMenu.cardDict.ContainsKey(cardId))
                {
                    if(MenuControl.Instance.checkIMPORTANT)
                    Debug.LogError(("Card not found: " + cardId));
                    continue;

                }
                res.Add(MenuControl.Instance.heroMenu.cardDict[cardId]);
            }

            return res;
        }
    }
    
    
    static public List<Card> SortCards(List<Card> cs)
    {
        return cs.OrderBy(x => x.CardTypeSortedValue() + x.UniqueID).ToList();
    }
    static public List<Card> SortCardsWithId(List<Card> cs)
    {
        return cs.OrderBy(x => x.CardTypeSortedValue() + x.UniqueID).ToList();
    }


    private Card downgradeCard
    {
        get
        {
            if (!MenuControl.Instance.csvLoader.cardUniqueIdToDowngradeCardUniqueId.ContainsKey(this.UniqueID))
            {
                return this;
            }

            var cardId = MenuControl.Instance.csvLoader.cardUniqueIdToDowngradeCardUniqueId[this.UniqueID];
            if (!MenuControl.Instance.heroMenu.cardDict.ContainsKey(cardId))
            {
                if (MenuControl.Instance.checkIMPORTANT)
                {
                    Debug.LogError(("Card not found: " + cardId));
                }
                return this;
                
            }
            return MenuControl.Instance.heroMenu.cardDict[cardId];
        }
    }

    public bool isCopiedCard = false;

    public bool isFlying = false;
public CardTargetType cardTargetType;
    public void StartFly()
    {
        isFlying = true;
        player.RenderCards();
    }

    public bool isAnyTypeFollows(CardType cardType)
    {
        if ((cardType & CardType.Minion) == CardType.Minion)
        {
            if (this is Minion)
            {
                return true;
            }
        }

        if ((cardType & CardType.Spell) == CardType.Spell)
        {
            if (isSpell())
            {
                return true;
            }
        }
        
        if ((cardType & CardType.Weapon) == CardType.Weapon)
        {
            if (isWeapon())
            {
                return true;
            }
        }
        
        if ((cardType & CardType.Item) == CardType.Item)
        {
            if (isItem)
            {
                return true;
            }
        }
        
        if ((cardType & CardType.Treasure) == CardType.Treasure)
        {
            if (IsTreasure())
            {
                return true;
            }
        }

        return false;
    }

    public bool isWeapon()
    {
        return this is NewWeapon;
    }
    public void StopFly()
    {
        isFlying = false;
        player.RenderCards();
    }

    public Card ancestorCard =>downgradeCard.downgradeCard;

    public Ability activatedAbility;

    public List<CardTag> cardTags = new List<CardTag>();

    public ActionAnimation actionAnimation;
    public bool isCardUnlockedAndAvailable => MenuControl.Instance.heroMenu.isCardUnlockedAndAvailable(this);
    
    public bool achievementRestricted=>false;
    public int goldWorth;

    public bool temporaryOnly;
    public string base_GetIDForOrderedList()
    {
        int count = 0;
        char[] numbers = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        foreach (char character in UniqueID)
        {
            foreach (char number in numbers){
                if (number == character)
                {
                    count += 1;
                }
            }
        }

        if (count >= 3)
        {
            return "0" + UniqueID;
        }

        return UniqueID;
    }
    public override string GetIDForOrderedList()
    {
        return ancestorCard.base_GetIDForOrderedList()+"_"+level;

    }

    public bool isConsumable => isPotion && GetComponent<Heal>() != null;
    public bool isPotion => cardTags.Contains(MenuControl.Instance.potionTag);
    public bool isItem => isPotion || this is Artifact;

    public bool isSpell()
    {
        return cardTags.Contains(MenuControl.Instance.spellTag);
    }

    public void ChangeManaCost(int value,Ability ability)
    {
        if (originalCost < 0)
        {
            originalCost = initialCost;
        }
        initialCost += value;
        foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
        {
            try
            {
                trigger.CardChangedInitialManaCost(this,ability,value);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }

        }
    }

    public void ResetCard()
    {
        if (originalCost < 0)
        {
            originalCost = initialCost;
        }
        initialCost = originalCost;
    }
    public int GetCost()
    {
        int newValue = initialCost;

        if (MenuControl.Instance.battleMenu.inBattle)
        {
            foreach (CostsModifier modifier in MenuControl.Instance.battleMenu.GetComponentsInChildren<CostsModifier>())
            {
                if (modifier.enabled)
                {
                    newValue = modifier.ModifyAmount(this, newValue);
                }
            }
        }

        return Mathf.Max(0, newValue);
    }

    public bool IsTreasure()
    {
        return this is Artifact || cardTags.Contains(MenuControl.Instance.treasureTag);
    }
    public void PutIntoZone(Zone zone,bool putAtTheEnd = false)
    {
        ResetCard();
        player.PutCardIntoZone(this, zone,putAtTheEnd);
    }

    public bool showUpgradeView()
    {
        if (this == null)
        {
            return false;
        }

        if (player == MenuControl.Instance.battleMenu
                .playerAI)
        {
            return false;
        }

        if (!MenuControl.Instance.csvLoader.isValidInCurrentVersion(UniqueID))
        {
            return false;
        }

        if (this is Hero)
        {
            return false;
        }

        if (this.IsTreasure() || this is Artifact || this.isPotion)
        {
            return false;
        }

        return true;
    }
    public List<string> GetCardTypes()
    {
        List<string> res = new List<string>();
       // string returnString = ""; //MenuControl.Instance.GetLocalizedString("Card");

        if (this is Minion)
        {
            res .Add( MenuControl.Instance.GetLocalizedString("Minion (Unit)"));
        }

        if (this is Artifact)
        {
            res.Add(MenuControl.Instance.GetLocalizedString("Treasure"));
        }
        if (this is Hero)
        {
            if (this is LargeHero)
            {
                res .Add( MenuControl.Instance.GetLocalizedString("Large Hero (Unit)"));
            }
            else
            {
                res .Add( MenuControl.Instance.GetLocalizedString("Hero (Unit)"));
            }
        }
        if (this is NewWeapon)
        {
            res .Add( MenuControl.Instance.GetLocalizedString("Weapon"));
        }
        if (this is Skill)
        {
            res .Add( MenuControl.Instance.GetLocalizedString("Skill"));
        }


        foreach (CardTag tag1 in cardTags)
        {
            if (tag1.visibleTag)
            {
                if (this is Artifact && tag1.name == "Spell")
                {
                    continue;
                }
                res .Add( MenuControl.Instance.GetLocalizedString(tag1.name));
            }
        }
        return res;
    }
    // public string GetCardTypeText()
    // {
    //     string returnString = ""; //MenuControl.Instance.GetLocalizedString("Card");
    //
    //     if (this is Minion)
    //     {
    //         returnString = MenuControl.Instance.GetLocalizedString("Minion (Unit)");
    //     }
    //     if (this is Hero)
    //     {
    //         if (this is LargeHero)
    //         {
    //             returnString = MenuControl.Instance.GetLocalizedString("Large Hero (Unit)");
    //         }
    //         else returnString = MenuControl.Instance.GetLocalizedString("Hero (Unit)");
    //     }
    //     if (this is NewWeapon)
    //     {
    //         returnString = MenuControl.Instance.GetLocalizedString("Weapon");
    //     }
    //     if (this is Skill)
    //     {
    //         returnString = MenuControl.Instance.GetLocalizedString("Skill");
    //     }
    //
    //
    //     foreach (CardTag tag1 in cardTags)
    //     {
    //         if (tag1.visibleTag)
    //         {
    //             if (this is Artifact && tag1.name == "Spell")
    //             {
    //                 continue;
    //             }
    //             returnString += (returnString == "" ? "" : ", ") + MenuControl.Instance.GetLocalizedString(tag1.name);
    //         }
    //     }
    //     return returnString;
    //
    // }

    public virtual bool CanTarget(Tile tile)
    {
        return false;
    }

    public virtual float PerformAnimationTime()
    {
        return 0;
    }
    public virtual void TargetTile(Tile tile, bool payCost)
    {

    }

    public void DrawThisCard()
    {
        MenuControl.Instance.progressMenu.discoverCard(this);
        player.PutCardIntoZone(this, MenuControl.Instance.battleMenu.hand);

        //Draw a card event trigger
        foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
        {
            try
            {
                trigger.CardDrawn(this);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }

        }
    }

    public bool CanBeDiscarded(bool autoEndTurn)
    {
        foreach (Ability ability in MenuControl.Instance.battleMenu.GetComponentsInChildren<Ability>())
        {
            if (!ability.CanDiscard(this, autoEndTurn)) return false;
        }

        return true;
    }

    public void DiscardThisCard(bool automaticDiscard = false)
    {
        player.PutCardIntoZone(this, MenuControl.Instance.battleMenu.discard);

        //discard a card event trigger
        foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
        {
            try
            {
                trigger.CardDiscarded(this, automaticDiscard);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }

        }
    }

    public void RazeThisCard()
    {
        ExhaustThisCard();

        foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
        {
            try
            {
                trigger.CardRazed(this);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }

        }
    }

    public bool CanAffordCost()
    {
        return MenuControl.Instance.settingsMenu.infinitMoveAndActions || player.GetCurrentMana() >= GetCost();
    }

    public int CardTypeSortedValue()
    {
        if (this is Hero)
        {
            return 0;
        }

        if (this is Minion)
        {
            return 1;
        }
        if (this is NewWeapon)
        {
            return 3;
        }

        return 2;
    }

    public Zone GetZone()
    {
        if (player == null) return null;

        if (player.cardsInDeck.Contains(this)) return MenuControl.Instance.battleMenu.deck;
        if (player.cardsInHand.Contains(this)) return MenuControl.Instance.battleMenu.hand;
        if (player.cardsOnBoard.Contains(this)) return MenuControl.Instance.battleMenu.board;
        if (player.cardsInDiscard.Contains(this)) return MenuControl.Instance.battleMenu.discard;
        if (player.cardsRemovedFromGame.Contains(this)) return MenuControl.Instance.battleMenu.removedFromGame;
        if (player.GetItems().Contains(this)) return MenuControl.Instance.battleMenu.artifact;

        return MenuControl.Instance.battleMenu.limbo;
    }

    public void ExhaustThisCard()
    {
        Zone previousZone = GetZone();
        if (previousZone == MenuControl.Instance.battleMenu.removedFromGame)
        {
            //avoid exhaust multiple times for sword dancegit pull
            return;
        }
        RemoveFromGame();

        foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
        {
            try
            {
                trigger.CardExhausted(this, previousZone);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }

        }
    }


    public void RemoveFromGame(bool consume = false)
    {
        player.PutCardIntoZone(this, MenuControl.Instance.battleMenu.removedFromGame);

        
        
        foreach (Card card in MenuControl.Instance.heroMenu.artifactsEquipped.ToArray())
        {
            if (card.isPotion &&  card.CardUniqueId == CardUniqueId)
            {
                MenuControl.Instance.heroMenu.RemoveCardFromDeck(card);
                    

                for (int i = 0; i < player.allCards.Count;i++)
                {
                    if (player.allCards[i].CardUniqueId == card.CardUniqueId)
                    {
                        player.allCards.RemoveAt(i);
                        break;
                    }
                }
                //player.allCards.Remove(card);
                    
                break;
            }
        }
        
        
        if (consume && player == MenuControl.Instance.battleMenu.player1)
        {
            foreach (Card card in MenuControl.Instance.heroMenu.cardsOwned.ToArray())
            {
                if (card.UniqueID == UniqueID && card.CardUniqueId == CardUniqueId)
                {
                    MenuControl.Instance.heroMenu.RemoveCardFromDeck(card);
                    break;
                }
            }

            foreach (Card card in MenuControl.Instance.heroMenu.artifactsOwned.ToArray())
            {
                if (card.UniqueID == UniqueID)
                {
                    MenuControl.Instance.heroMenu.RemoveCardFromDeck(card);
                    break;
                }
            }
            
        }

        //remove a card event trigger
        foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
        {
            try
            {
                trigger.CardRemoved(this);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }

        }
    }
    public bool IsDiscovered()
    {
        //return !MenuControl.Instance.csvLoader.isLocked(GetChineseName()) || (MenuControl.Instance.heroMenu.unlockableCards.Contains(this) && MenuControl.Instance.heroMenu.startingCardsUnlockedNames.Contains( GetDowngradeChineseName())) || MenuControl.Instance.libraryMenu.showLocked;
        var isUnlocked =  MenuControl.Instance.heroMenu.unlockableCards.Contains(this) &&
                           MenuControl.Instance.heroMenu.startingCardsUnlockedNames.Contains( UniqueID);
        var showLocked = MenuControl.Instance.libraryMenu.showLocked;
        if (isUnlocked || showLocked)
        {
            return true;
        }
        
        if (downgradeCard != null)
        {
            if (MenuControl.Instance.progressMenu.cardsDiscovered.Contains(downgradeCard.UniqueID)) return true;
        
            Card firstCard = downgradeCard.downgradeCard;
            if (firstCard != null)
            {
                if (MenuControl.Instance.progressMenu.cardsDiscovered.Contains(firstCard.UniqueID)) return true;
            }
        }
        if (upgradeCards.Count>0)
        {
            foreach (var upgradeCard in upgradeCards)
            {
                if (MenuControl.Instance.progressMenu.cardsDiscovered.Contains(upgradeCard.UniqueID)) return true;
                if (upgradeCard.upgradeCards.Count > 0)
                {
                    foreach (var upgradeCard2 in upgradeCard.upgradeCards)
                    {
                        if (MenuControl.Instance.progressMenu.cardsDiscovered.Contains(upgradeCard2.UniqueID))
                            return true;
                    }
                }
                // Card firstCard = upgradeCard;
                // if (firstCard != null)
                // {
                //     if (MenuControl.Instance.progressMenu.cardsDiscovered.Contains(firstCard.UniqueID)) return true;
                // }
            }
        }

        return true;
        // return MenuControl.Instance.progressMenu.cardsDiscovered.Contains(this.UniqueID);
    }

    public int GetPointScore()
    {
        try
        {
            foreach (string[] dropRateLineData in MenuControl.Instance.heroMenu.dropRateLines)
            {

                if (dropRateLineData[0] == UniqueID)
                {
                    if (dropRateLineData.Length >= 2 && dropRateLineData[1] != "")
                        return Mathf.Max(1, int.Parse(dropRateLineData[1]));
                    else return 8;
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }

        return 8;

    }

    public int GetBaseDropScore()
    {
        try
        {
            foreach (string[] dropRateLineData in MenuControl.Instance.heroMenu.dropRateLines)
            {

                if (dropRateLineData[0] == UniqueID)
                {

                    if (dropRateLineData.Length >= 4 && dropRateLineData[3] != "")
                        return Mathf.Max(1, int.Parse(dropRateLineData[3]));
                    else return 50;
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }

        return 50;
 
    }

    public bool IsCurrentClassCard()
    {
        
        var info = MenuControl.Instance.csvLoader.uniqueIdToPlayerCardInfo[UniqueID];
        if (info.hero == 0 || info.hero == MenuControl.Instance.heroMenu.getCurrentClassIndexReal()||(info.hero== -1 &&( info.path == 0 || info.path ==  MenuControl.Instance.heroMenu.getCurrentClassIndexReal())))
        {
            return true;
        }

        return false;
    }
    public int GetPremiumScore()
    {
        try
        {
            foreach (string[] dropRateLineData in MenuControl.Instance.heroMenu.dropRateLines)
            {

                if (dropRateLineData[0] == UniqueID)
                {
                    if (dropRateLineData.Length >= 5 && dropRateLineData[4] != "")
                        return Mathf.Max(1, int.Parse(dropRateLineData[4]));
                    else return 50;
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }

        return 50;

    }

    public List<string> GetSynergyTags()
    {
        List<string> synergyTags = new List<string>();
        try
        {
            foreach (string[] dropRateLineData in MenuControl.Instance.heroMenu.dropRateLines)
            {

                if (dropRateLineData[0] == UniqueID && dropRateLineData.Length >= 3)
                {
                    string combinedString = dropRateLineData[2].Trim('"');
                    string[] tagStrings =  (combinedString.Trim()).Split(","[0]);
                    foreach (string tagString in tagStrings)
                    {
                        if (tagString.Trim() != "")
                            synergyTags.Add(tagString.Trim());
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }

        return synergyTags;
    }



}
