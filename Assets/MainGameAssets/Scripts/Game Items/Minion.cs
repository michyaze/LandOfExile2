using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : Unit
{
    public bool exhaustAfterUsage = true;

    public override void SufferDamage(Card sourceCard, Ability ability, int damageAmount, bool destroy = false, bool triggerUnitDamage = true, bool firstDamage = true)
    {
        base.SufferDamage(sourceCard, ability, damageAmount, destroy, triggerUnitDamage,firstDamage);

        if (GetHP() <= 0 && GetZone() == MenuControl.Instance.battleMenu.board)
        {

            foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
            {
                try
                {
                    trigger.MinionDestroyed(sourceCard, ability, damageAmount, this);
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                }

            }

            if (MenuControl.Instance.heroMenu.reaperMode && player == MenuControl.Instance.battleMenu.player1 && GetZone() != MenuControl.Instance.battleMenu.hand)
            {
                if (!MenuControl.Instance.heroMenu.heroClasses[1].classCards.Contains(MenuControl.Instance.heroMenu.GetCardByID( cardTemplate.UniqueID)) && UniqueID != "Treasure32a" && !MenuControl.Instance.heroMenu.GetAllUnlockedTreasures().Contains(MenuControl.Instance.heroMenu.GetCardByID(cardTemplate.UniqueID)))
                {
                    
                    int shopIndex = MenuControl.Instance.adventureMenu.adventureItems.IndexOf(MenuControl.Instance.adventureMenu.GetItemOfType<AdventureItemPurchaseCardsReaper>());

                    if (shopIndex >= 0 && MenuControl.Instance.heroMenu.DeckContainsCardTemplate(cardTemplate))
                    {
                        List<Card> shopCards = new List<Card>();
                        for (int ii = 0; ii < MenuControl.Instance.adventureMenu.itemCardsForItemIndex.Count; ii++)
                        {
                            shopCards.Add(MenuControl.Instance.adventureMenu.itemCards[ii]);
                        }
                        if (!shopCards.Contains(MenuControl.Instance.heroMenu.GetCardByID(cardTemplate.UniqueID)))
                        {

                            MenuControl.Instance.adventureMenu.itemCards.Add(MenuControl.Instance.heroMenu.GetCardByID(cardTemplate.UniqueID));
                            MenuControl.Instance.adventureMenu.addToItemCardsForItemIndex(shopIndex);
                        }
                    }

                    RemoveFromGame(true);
                    return;
                }
            }


            removeMinion();

        }
    }

    public void Sacrifice(Card sourceCard, Ability ability)
    {
        foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
        {
            try
            {
                trigger.MinionSacrificed(sourceCard, ability, this);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }

        }

        foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
        {
            try
            {
                trigger.MinionDestroyed(sourceCard, ability, currentHP, this);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }

        }

        removeMinion();
    }

    public override void ChangeCurrentHP(Ability ability, int newValue)
    {
        base.ChangeCurrentHP(ability, newValue);

        
        CheckMinionHPAndRemove(ability);
    }

    public override Effect ApplyEffect(Card sourceCard, Ability ability, Effect effectTemplate, int charges)
    {
        Effect returnEffect = base.ApplyEffect(sourceCard, ability, effectTemplate, charges);
        

        
        CheckMinionHPAndRemove(ability);

        return returnEffect;
    }

    public override void RemoveEffect(Card sourceCard, Ability ability, Effect effect)
    {
        base.RemoveEffect(sourceCard, ability, effect);

        CheckMinionHPAndRemove(ability);
    }

    void CheckMinionHPAndRemove(Ability ability)
    {
        if (GetHP() <= 0 && GetZone() == MenuControl.Instance.battleMenu.board)
        {

            foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
            {
                try
                {
                    trigger.MinionDestroyed(ability.GetCard(), ability, 0, this);
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                }

            }

            removeMinion();

        }
    }

    void removeMinion()
    {
        if (exhaustAfterUsage && MenuControl.Instance.exhustAllMinionAfterUsage)
        {
            ExhaustThisCard();
        }
        else
        {//已经exhaust的卡不会再次discard
            if (GetZone() != MenuControl.Instance.battleMenu.removedFromGame)
            {
                player.PutCardIntoZone(this, MenuControl.Instance.battleMenu.discard);

                InitializeUnit(false);
            }
        }
    }
    

    public override int GetInitialPower(bool ignoreBattle = false, Player playerOverride = null)
    {
        bool is2ndHandCard = false;
        if (MenuControl.Instance.battleMenu.inBattle)
        {
            foreach (VisibleCard vc in MenuControl.Instance.battleMenu.playerAI.intentSystemNextHandVisibleCards)
            {
                if (vc.card == this && cardTemplate == null)
                {
                    is2ndHandCard = true;
                }
            }
        }

        if (MenuControl.Instance.heroMenu.ascensionMode >= 14 && (!MenuControl.Instance.battleMenu.inBattle || ignoreBattle || is2ndHandCard))
        {
            if (!MenuControl.Instance.battleMenu.inBattle)
            {
                if (!MenuControl.Instance.deckMenu.gameObject.activeInHierarchy || !MenuControl.Instance.deckMenu.titleText.text.Contains(MenuControl.Instance.GetLocalizedString("Enemy Deck")))
                {
                    return base.GetInitialPower(ignoreBattle, null);
                }
            }

            if (MenuControl.Instance.areaMenu.areasVisited == 3 && MenuControl.Instance.battleMenu.player1 != playerOverride)
            {
                return base.GetInitialPower(ignoreBattle, null) + 1;
            }
        }

        return base.GetInitialPower(ignoreBattle, null);
    }

    public override int GetBaseHP(bool ignoreBattle = false, Player playerOverride = null)
    {
        bool is2ndHandCard = false;
        if (MenuControl.Instance.battleMenu.inBattle)
        {
            foreach (VisibleCard vc in MenuControl.Instance.battleMenu.playerAI.intentSystemNextHandVisibleCards)
            {
                if (vc.card == this && cardTemplate == null)
                {
                    is2ndHandCard = true;
                }
            }
        }
        if (MenuControl.Instance.heroMenu.ascensionMode >= 14 && (!MenuControl.Instance.battleMenu.inBattle || ignoreBattle || is2ndHandCard))
        {
            if (!MenuControl.Instance.battleMenu.inBattle)
            {
                if (!MenuControl.Instance.deckMenu.gameObject.activeInHierarchy || !MenuControl.Instance.deckMenu.titleText.text.Contains(MenuControl.Instance.GetLocalizedString("Enemy Deck")))
                {
                    return base.GetBaseHP(ignoreBattle, null);
                }
            }

            if (MenuControl.Instance.battleMenu.player1 != playerOverride)
            {
                if (MenuControl.Instance.areaMenu.areasVisited == 1)
                {
                    return base.GetBaseHP(ignoreBattle, null) + 1;
                }
                if (MenuControl.Instance.areaMenu.areasVisited == 2)
                {
                    return base.GetBaseHP(ignoreBattle, null) + 2;
                }
                if (MenuControl.Instance.areaMenu.areasVisited == 3)
                {
                    return base.GetBaseHP(ignoreBattle, null) + 2;
                }
            }
        }

        return base.GetBaseHP(ignoreBattle, null);
    }

}
