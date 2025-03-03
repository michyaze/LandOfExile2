using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public List<Card> allOwnedCards = new List<Card>();
    public List<Card> allCards = new List<Card>();

    public List<Card> cardsInDeck = new List<Card>();
    public List<Card> cardsInHand = new List<Card>();
    public List<Card> cardsOnBoard = new List<Card>();
    public List<Card> cardsInDiscard = new List<Card>();
    public List<Card> cardsRemovedFromGame = new List<Card>();

    public float minIntervalOfCards = 150;
    public float maxIntervalOfCards = 300;
    

    public int baseDrawsPerTurn;

    
    
    public int currentMana;
    public int initialMana;

    public Transform cardsInGameHolder;
    public Transform visibleHandCardsHolder;
    public Transform visibleBoardCardsHolder;

    public Text manaText;
    public Transform manaIconsOn;
    public Transform manaIconsOff;
    public List<VisibleCard> visibleHandCards = new List<VisibleCard>();
    public List<VisibleCard> visibleBoardCards = new List<VisibleCard>();

    public Text deckPileText;
    public Text discardPileText;

    public List<VisibleCard> ghostCards = new List<VisibleCard>();

    public Transform intentSystemNextHandHolder;
    public List<VisibleCard> intentSystemNextHandVisibleCards = new List<VisibleCard>();

    public int GetCurrentMana()
    {
        int newHP = currentMana;
        if (MenuControl.Instance.battleMenu.inBattle)
        {
            foreach (ManaModifier modifier in MenuControl.Instance.battleMenu.GetComponentsInChildren<ManaModifier>())
            {
                if (modifier.enabled)
                {
                    newHP = modifier.ModifyAmount(this, newHP);
                }
            }
        }

        newHP = Mathf.Max(newHP, 0);

        return newHP;
    }
    
    public int GetInitialMana()
    {
        int newHP = initialMana;
        if (MenuControl.Instance.battleMenu.inBattle)
        {
            foreach (MaxManaModifier modifier in MenuControl.Instance.battleMenu.GetComponentsInChildren<MaxManaModifier>())
            {
                if (modifier.enabled)
                {
                    newHP = modifier.ModifyAmount(this, newHP);
                }
            }
        }

        newHP = Mathf.Max(newHP, 0);

        return newHP;
    }
    public void ChangeCardsInHand(List<Card> newCards)
    {
        for (int i = 0; i < cardsInHand.Count; i++)
        {
            var oldCard = cardsInHand[i];
            Destroy(oldCard.gameObject);
        }
        cardsInHand.Clear();
        for (int j = 0; j < newCards.Count; j++)
        {
                var newCard  =CreateCardInGameFromTemplate(newCards[j]);
                cardsInHand.Add(newCard);
        }
    }
    
    
    public void ChangeCardsInHand(List<Card> originalCards, List<Card> newCards)
    {
        for (int j = 0; j < originalCards.Count; j++)
        {
        for (int i = 0; i < cardsInHand.Count; i++)
        {
                if (cardsInHand[i].UniqueID == originalCards[j].UniqueID)
                {
                    var oldCard = cardsInHand[i];
                    var newCard  =CreateCardInGameFromTemplate(newCards[j]);
                    cardsInHand[i] = newCard;
                    Destroy(oldCard.gameObject);
                    break;
                }
            }
        }
    }

    public Card CreateCardInGameFromTemplate(Card templateCard)
    {
        Card card = Instantiate(templateCard, cardsInGameHolder);
        card.temporaryOnly = true;
        card.cardTemplate = templateCard;
        card.player = this;
        allCards.Add(card);

        if (card is Hero)
        {
            Hero hero = (Hero)card;
            if (this == MenuControl.Instance.battleMenu.player1)
            {
                hero.currentPower = ((Hero)templateCard).initialPower;
                hero.currentHP = ((Hero)templateCard).currentHP;
            }
            hero.remainingMoves = 0;
            hero.remainingActions = 0;
            hero.intentSystem = hero.GetComponent<IntentSystem>();

            //Create weapon
            if (hero.weapon)
            {
                hero.ChangeWeapon(hero.weapon);
            }

            //Apply starting effects
            for (int ii = 0; ii < hero.startingEffects.Count; ii += 1)
            {
                Effect effect = hero.startingEffects[ii];
                hero.ApplyEffect(hero, null, effect, hero.startingEffectCharges[ii]);
            }
            
            //apply temp effect
            for (int ii = 0; ii < hero.tempStartingEffects.Count; ii += 1)
            {
                Effect effect = hero.tempStartingEffects[ii];
                hero.ApplyEffect(hero, null, effect, hero.tempStartingEffectCharges[ii]);
            }

        }
        else if (card is Unit)
        {
            //使用 MagicS09b上的怪物会自动清除上的时候获得的飞行
            ((Unit)card).InitializeUnit(true);
        }

        return card;
    }

    public int GetDrawsPerTurn()
    {
        //TODO accept modifiers
        return baseDrawsPerTurn;
    }

    public void PayCostFor(Card card)
    {
        ChangeMana(-card.GetCost());
    }

    public void PutCardIntoZone(Card card, Zone zone, bool PutAtTheEnd = false)
    {
        if (card.GetZone() == zone) return;
        card.ResetCard();
        var previousZone = card.GetZone();
        if (card.GetZone() != null)
        {
            //Remove it from the 
            if (cardsInDeck.Contains(card))
                cardsInDeck.Remove(card);
            if (cardsInHand.Contains(card))
                cardsInHand.Remove(card);
            if (cardsOnBoard.Contains(card))
            {
                cardsOnBoard.Remove(card);
                MenuControl.Instance.battleMenu.boardMenu.RemoveUnitFromBoard((Unit)card);
            }
            if (cardsInDiscard.Contains(card))
                cardsInDiscard.Remove(card);
            if (cardsRemovedFromGame.Contains(card))
                cardsRemovedFromGame.Remove(card);
        }

        if (zone == MenuControl.Instance.battleMenu.deck)
        {
            if (MenuControl.Instance.battleMenu.usingIntentSystem && MenuControl.Instance.battleMenu.playerAI == this)
                cardsRemovedFromGame.Add(card);
            else
            {
                if (MenuControl.Instance.battleMenu.tutorialMode || PutAtTheEnd)
                {
                    cardsInDeck.Add(card);
                }
                else
                {
                    
                    cardsInDeck.InsertAtRandom(card);
                }
            }
        }
        else if (zone == MenuControl.Instance.battleMenu.hand)
        {
            cardsInHand.Add(card);

            if (previousZone != MenuControl.Instance.battleMenu.deck && card is Unit unit)
            {
                //触发回到手牌
                foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
                {
                    try
                    {
                        trigger.UnitReturnToHand(unit,this);
                    }
                    catch (System.Exception e)
                    {
                        Debug.LogError(e);
                    }

                }
            }
        }
        else if (zone == MenuControl.Instance.battleMenu.board)
        {
            cardsOnBoard.Add(card);
        }
        else if (zone == MenuControl.Instance.battleMenu.discard)
        {
            if (MenuControl.Instance.battleMenu.usingIntentSystem && MenuControl.Instance.battleMenu.playerAI == this)
                cardsRemovedFromGame.Add(card);
            else
                cardsInDiscard.Add(card);
        }
        else if (zone == MenuControl.Instance.battleMenu.removedFromGame)
        {
            cardsRemovedFromGame.Add(card);
        }

    }

    public void RenderIntent2ndHand()
    {

        List<Card> nextHandCards = new List<Card>();
        if (MenuControl.Instance.battleMenu.currentRound > 0) nextHandCards.AddRange(GetHero().GetIntentSystem().GetNextHand().GetFollowingHandCards());

        int cardsToAddOrRemove = nextHandCards.Count - intentSystemNextHandVisibleCards.Count;

        if (cardsToAddOrRemove > 0)
        {
            //Adding
            for (int ii = 0; ii < cardsToAddOrRemove; ii += 1)
            {
                VisibleCard visibleCard = Instantiate(MenuControl.Instance.visibleCardPrefab, intentSystemNextHandHolder);
                
                visibleCard.isEnemyIntentCard = true;
                visibleCard.isEnemy = true;
                intentSystemNextHandVisibleCards.Add(visibleCard);

                visibleCard.transform.position = deckPileText.transform.position;
                visibleCard.transform.localScale = Vector3.one * (MenuControl.Instance.battleMenu.player1 == this ? 1f : 0.8f);
                visibleCard.GetComponent<CanvasGroup>().alpha = 0f;
                LeanTween.alphaCanvas(visibleCard.GetComponent<CanvasGroup>(), 1f, MenuControl.Instance.battleMenu.GetPlaySpeed());
                visibleCard.transform.localScale = Vector3.one * 0.5f;

            }

        }
        if (cardsToAddOrRemove < 0)
        {
            //Removing
            for (int ii = 0; ii < -cardsToAddOrRemove; ii += 1)
            {
                Destroy(intentSystemNextHandHolder.GetChild(intentSystemNextHandVisibleCards.Count - 1).gameObject);
                intentSystemNextHandVisibleCards.RemoveAt(intentSystemNextHandVisibleCards.Count - 1);
            }
        }
        for (int ii = 0; ii < nextHandCards.Count; ii += 1)
        {
            Card card = nextHandCards[ii];
            VisibleCard vc = intentSystemNextHandVisibleCards[ii];
            vc.isEnemyIntentCard = true;
            vc.isEnemy = true;
            vc.RenderCardForMenu(card);

            float totalWidth = intentSystemNextHandHolder.GetComponent<RectTransform>().rect.width;

            float seperation = totalWidth / (float)(nextHandCards.Count + 1);

            if (seperation < minIntervalOfCards)
            {
                seperation = minIntervalOfCards;
                totalWidth = seperation * (float)(nextHandCards.Count + 1);
            }
            if (seperation > maxIntervalOfCards)
            {
                seperation = maxIntervalOfCards;
                totalWidth = seperation * (float)(nextHandCards.Count + 1);
            }

            int index = ii;

            float xPos = -totalWidth / 2f + ((index + 1) * seperation);

            vc.transform.SetSiblingIndex(index);

            Vector2 newPos = new Vector2(xPos + intentSystemNextHandHolder.transform.position.x, 70f - Mathf.Abs(xPos / (totalWidth / 2f)) * 70f);
            Vector3 newRot = new Vector3(0f, 0f, -xPos / (totalWidth / 2f) * 30f);

            LeanTween.scale(vc.GetComponent<RectTransform>(), Vector3.one * 0.6f, MenuControl.Instance.battleMenu.GetPlaySpeed()).setEaseInOutQuad().setDelay(index * (0.3f / nextHandCards.Count));
            LeanTween.move(vc.GetComponent<RectTransform>(), newPos, MenuControl.Instance.battleMenu.GetPlaySpeed()).setEaseInOutQuad().setDelay(index * (0.3f / nextHandCards.Count));
            LeanTween.rotateLocal(vc.gameObject, newRot, MenuControl.Instance.battleMenu.GetPlaySpeed()).setEaseInOutSine().setDelay(index * (0.3f / nextHandCards.Count));

        }

    }

    public void ShowDialogue(Card card,DialogueInBattleInfo info)
    {

        foreach (VisibleCard vcard in visibleBoardCards.ToArray())
        {
            if (vcard.card.UniqueID == card.UniqueID)
            {
                vcard.ShowDialogue(info);
            }
        }
    }
    
    public void RenderCards(bool noBoardMovement = false)
    {
        //Update mana indicator
        manaText.text = GetCurrentMana().ToString();// + "/" + initialMana.ToString();
        if (manaIconsOn != null)
        {
            for (int ii = 0; ii < manaIconsOn.childCount; ii += 1)
            {
                manaIconsOn.GetChild(ii).gameObject.SetActive(GetCurrentMana() > ii);
            }
        }

        if (manaIconsOff != null)
        {
            for (int ii = 0; ii < manaIconsOff.childCount; ii += 1)
            {
                manaIconsOff.GetChild(ii).gameObject.SetActive(GetInitialMana() > ii);
            }
        }

        //Layout Hand
        List<Card> handCardsToRender = new List<Card>();
        handCardsToRender.AddRange(cardsInHand);

        int cardsDiscarded = 0;
        foreach (VisibleCard card in visibleHandCards.ToArray())
        {
            if (!cardsInHand.Contains(card.card))
            {
                // this is to take care when first equip weapon, should not show discard +1, a quite hacky way I'd say
                // and it would cause it never show +1 for weapon change later.. a bug.
                
                // the later one is to prevent show +1 on discard pile when remove a card
                if (card.card.GetZone() != MenuControl.Instance.battleMenu.limbo && card.card.GetZone() != MenuControl.Instance.battleMenu.removedFromGame&& card.card.GetZone() != MenuControl.Instance.battleMenu.board)
                {
                    cardsDiscarded += 1;
                }
                visibleHandCards.Remove(card);
                Destroy(card.gameObject);
            }
            else
            {
                handCardsToRender.Remove(card.card);
                card.RenderCard(card.card);
                card.isMenuCard = false;
            }
        }

        if (handCardsToRender.Count > 0)
            MenuControl.Instance.battleMenu.boardMenu.CardsDrawnEffect(this, handCardsToRender.Count);

        foreach (Card card in handCardsToRender)
        {
            VisibleCard visibleCard = Instantiate(MenuControl.Instance.visibleCardPrefab, visibleHandCardsHolder);

            bool fromIntent2ndHand = false;
            if (MenuControl.Instance.battleMenu.usingIntentSystem && this == MenuControl.Instance.battleMenu.playerAI)
            {

                foreach (VisibleCard vc in intentSystemNextHandVisibleCards)
                {
                    if (vc.card.UniqueID == card.UniqueID)
                    {
                        visibleCard.transform.position = vc.transform.position;
                        visibleCard.transform.rotation = vc.transform.rotation;
                        visibleCard.transform.localScale = vc.transform.localScale;

                        intentSystemNextHandVisibleCards.Remove(vc);
                        Destroy(vc.gameObject);
                        fromIntent2ndHand = true;
                        break;
                    }
                }
            }

            if (!fromIntent2ndHand)
            {
                visibleCard.transform.position = deckPileText.transform.position;
                visibleCard.transform.localScale = Vector3.one * (MenuControl.Instance.battleMenu.player1 == this ? 1f : 0.8f);
                visibleCard.GetComponent<CanvasGroup>().alpha = 0f;
                LeanTween.alphaCanvas(visibleCard.GetComponent<CanvasGroup>(), 1f, MenuControl.Instance.battleMenu.GetPlaySpeed());
                visibleCard.transform.localScale = Vector3.one * 0.5f;
            }

            visibleCard.RenderCard(card);
            visibleHandCards.Add(visibleCard);

            int index = cardsInHand.IndexOf(card);
            LeanTween.delayedCall(index * (0.6f / cardsInHand.Count), () =>
            {
                Doozy.Engine.Soundy.SoundyManager.Play(MenuControl.Instance.battleMenu.drawCardSound);
            });
        }


        for (int ii = 0; ii < cardsInHand.Count; ii += 1)
        {
            Card card = cardsInHand[ii];
            VisibleCard vc = GetVisibleCardForCard(card);


            vc.disableInteraction = MenuControl.Instance.battleMenu.playerAI == this && !MenuControl.Instance.battleMenu.usingIntentSystem;


            float totalWidth = visibleHandCardsHolder.GetComponent<RectTransform>().rect.width;

            float seperation = totalWidth / (float)(cardsInHand.Count + 1);

            if (seperation < minIntervalOfCards)
            {
                seperation = minIntervalOfCards;
                totalWidth = seperation * (float)(cardsInHand.Count + 1);
            }
            if (seperation > maxIntervalOfCards)
            {
                seperation = maxIntervalOfCards;
                totalWidth = seperation * (float)(cardsInHand.Count + 1);
            }

            int index = ii;

            float xPos = -totalWidth / 2f + ((index + 1) * seperation);

            vc.transform.SetSiblingIndex(index);

            Vector2 newPos = new Vector2(xPos, 70f - Mathf.Abs(xPos / (totalWidth / 2f)) * 70f);
            Vector3 newRot = new Vector3(0f, 0f, -xPos / (totalWidth / 2f) * 30f);

            LeanTween.scale(vc.GetComponent<RectTransform>(), Vector3.one * (MenuControl.Instance.battleMenu.player1 == this ? 1f : 0.8f), MenuControl.Instance.battleMenu.GetPlaySpeed()).setEaseInOutQuad().setDelay(index * (0.3f / cardsInHand.Count));
            LeanTween.move(vc.GetComponent<RectTransform>(), newPos, MenuControl.Instance.battleMenu.GetPlaySpeed()).setEaseInOutQuad().setDelay(index * (0.3f / cardsInHand.Count));
            LeanTween.rotateLocal(vc.gameObject, newRot, MenuControl.Instance.battleMenu.GetPlaySpeed()).setEaseInOutSine().setDelay(index * (0.3f / cardsInHand.Count));

        }


        //Layout Board
        List<Card> boardCardsToRender = new List<Card>();
        boardCardsToRender.AddRange(cardsOnBoard);

        foreach (VisibleCard card in visibleBoardCards.ToArray())
        {
            if (!cardsOnBoard.Contains(card.card))
            {
                cardsDiscarded += 1;
                visibleBoardCards.Remove(card);
                Destroy(card.gameObject);

            }
            else
            {
                boardCardsToRender.Remove(card.card);
                card.RenderCardOnBoard(card.card);
            }
        }

        foreach (Card card in boardCardsToRender)
        {
            VisibleCard visibleCard = Instantiate(card is LargeHero ? MenuControl.Instance.battleMenu.visibleLargeHeroPrefab : MenuControl.Instance.visibleCardPrefab, visibleBoardCardsHolder);
            visibleCard.RenderCardOnBoard(card);
            visibleBoardCards.Add(visibleCard);
            visibleCard.transform.position = ((Unit)card).GetTile().transform.position;// visibleHandCardsHolder.transform.position; //To appear moving from hand
            visibleCard.transform.localScale = Vector2.one * 0.9f;
        }

        if (!noBoardMovement)
        {
            foreach (VisibleCard vc in visibleBoardCards)
            {
                LeanTween.cancel(vc.gameObject);

                Tile tile = ((Unit)vc.card).GetTile();
                if (tile == null)
                {
                    Debug.LogError("how can this be null?");
                }
                else
                {
                    LeanTween.move(vc.GetComponent<RectTransform>(), tile.transform.localPosition, Mathf.Min(0.35f, MenuControl.Instance.battleMenu.GetPlaySpeed())).setEaseInOutSine();
                    LeanTween.rotateLocal(vc.gameObject, Vector3.zero, Mathf.Min(0.35f, MenuControl.Instance.battleMenu.GetPlaySpeed())).setEaseInOutSine();
                    LeanTween.scale(vc.gameObject, Vector2.one * 0.9f, Mathf.Min(0.35f, MenuControl.Instance.battleMenu.GetPlaySpeed())).setEaseInOutSine();

                }
                 }
        }

        if (cardsDiscarded > 0)
            MenuControl.Instance.battleMenu.boardMenu.CardsDiscardedEffect(this, cardsDiscarded);

        deckPileText.text = "x" + cardsInDeck.Count.ToString();
        discardPileText.text = "x" + cardsInDiscard.Count.ToString();
    }

    public void ChangeMana(int amountToAdjust)
    {
        int previousAmount = currentMana;
        currentMana += amountToAdjust;
        //if (currentMana < 0) currentMana = 0;

        foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
        {
            try
            {
                trigger.ManaChanged(this, amountToAdjust, previousAmount);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }

        }
    }

    public VisibleCard GetVisibleBoardCardForCard(Card card)
    {
        VisibleCard vc = null;
        foreach (VisibleCard vCInHand in visibleBoardCards)
        {
            if (vCInHand.card == card)
            {
                vc = vCInHand;
                break;
            }
        }
        return vc;
    }

    public VisibleCard GetVisibleCardForCard(Card card)
    {
        VisibleCard vc = null;
        foreach (VisibleCard vCInHand in visibleHandCards)
        {
            if (vCInHand.card == card)
            {
                vc = vCInHand;
                break;
            }
        }
        return vc;
    }

    public void ResetBattle()
    {
        HideGhostHand();
        foreach (Card card in allCards.ToArray())
        {
            if (card != null)
            {
                Destroy(card.gameObject);
            }
        }
        allCards.Clear();
        cardsInDeck.Clear();
        cardsInHand.Clear();
        cardsOnBoard.Clear();
        cardsInDiscard.Clear();
        cardsRemovedFromGame.Clear();

        foreach (VisibleCard vc in visibleHandCards)
        {
            if (vc != null)
                Destroy(vc.gameObject);
        }
        foreach (VisibleCard vc in visibleBoardCards)
        {
            if (vc != null)
                Destroy(vc.gameObject);
        }
        visibleHandCards.Clear();
        visibleBoardCards.Clear();

        foreach (VisibleCard vc in intentSystemNextHandVisibleCards)
        {
            if (vc != null)
                Destroy(vc.gameObject);
        }
        intentSystemNextHandVisibleCards.Clear();

        manaText.text = "";
        discardPileText.text = "";
        deckPileText.text = "";


    }

    public void ShuffleDeck()
    {
        cardsInDeck.Shuffle();
        //BroadCast
        foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
        {
            try
            {
                trigger.PlayerShuffledDeck(this);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }

        }

    }

    public void DrawACard()
    {
        if (cardsInDeck.Count > 0)
        {
            //Card card = cardsInDeck.PickItem();//[0];
            Card card = cardsInDeck[0];
            card.DrawThisCard();
        }
        else
        {
            ShuffleDiscardIntoDeck();
            if (cardsInDeck.Count > 0)
            {
                //Card card = cardsInDeck.PickItem();//[0];
                Card card = cardsInDeck[0];
                card.DrawThisCard();
            }
        }
    }

    public void ShuffleDiscardIntoDeck()
    {
        foreach (Card card in cardsInDiscard.ToArray())
        {
            PutCardIntoZone(card, MenuControl.Instance.battleMenu.deck);
        }

        ShuffleDeck();
    }

    public Hero GetHero()
    {

        foreach (Card card in cardsOnBoard)
        {
            if (card is Hero) return (Hero)card;
        }

        return null;
    }

    public Player GetOpponent()
    {
        if (MenuControl.Instance.battleMenu.player1 == this)
            return MenuControl.Instance.battleMenu.playerAI;

        return MenuControl.Instance.battleMenu.player1;
    }

    public void ShowDiscardPile()
    {
        if (cardsInDiscard.Count > 0)
        {
            List<Card> cards = new List<Card>();
            foreach (Card card in cardsInDiscard)
            {
                cards.Add(card);
                //cards.Add(MenuControl.Instance.heroMenu.GetCardByID(card.UniqueID));
            }

            MenuControl.Instance.deckMenu.ShowDeck(cards, MenuControl.Instance.GetLocalizedString(this == MenuControl.Instance.battleMenu.player1 ? "My Discard Pile" : "Enemy Discard Pile"));
        }
        else
        {
            RectTransform rect = discardPileText.transform.parent.GetComponent<RectTransform>();
            LeanTween.move(rect, rect.anchoredPosition + Vector2.right * 30f, 0.07f).setEaseInOutSine().setLoopPingPong(3);
        }
    }

    public void GhostDrawNextHand()
    {

        int amountToDraw = GetDrawsPerTurn();
        for (int ii = 0; ii < amountToDraw; ii += 1)
        {
            VisibleCard ghostCard = Instantiate(MenuControl.Instance.visibleCardPrefab, visibleHandCardsHolder);
            ghostCard.RenderGhostCard();
            ghostCard.transform.position = deckPileText.transform.position;


            float totalWidth = visibleHandCardsHolder.GetComponent<RectTransform>().rect.width;

            float seperation = totalWidth / (float)(amountToDraw + 1);

            if (seperation < minIntervalOfCards)
            {
                seperation = minIntervalOfCards;
                totalWidth = seperation * (float)(amountToDraw + 1);
            }
            if (seperation > maxIntervalOfCards)
            {
                seperation = maxIntervalOfCards;
                totalWidth = seperation * (float)(amountToDraw + 1);
            }

            int index = ii;

            float xPos = -totalWidth / 2f + ((index + 1) * seperation);

            Vector2 newPos = new Vector2(xPos, 70f - Mathf.Abs(xPos / (totalWidth / 2f)) * 70f);
            Vector3 newRot = new Vector3(0f, 0f, -xPos / (totalWidth / 2f) * 30f);

            LeanTween.move(ghostCard.GetComponent<RectTransform>(), newPos, MenuControl.Instance.battleMenu.GetPlaySpeed()).setEaseInOutQuad().setDelay(index * (0.3f / amountToDraw));
            LeanTween.rotateLocal(ghostCard.gameObject, newRot, MenuControl.Instance.battleMenu.GetPlaySpeed()).setEaseInOutSine().setDelay(index * (0.3f / amountToDraw));

            ghostCard.GetComponent<CanvasGroup>().alpha = 0f;
            LeanTween.alphaCanvas(ghostCard.GetComponent<CanvasGroup>(), 1f, MenuControl.Instance.battleMenu.GetPlaySpeed());

            ghostCard.transform.localScale = Vector3.one * 0.5f;
            LeanTween.scale(ghostCard.gameObject, Vector3.one, MenuControl.Instance.battleMenu.GetPlaySpeed()).setEaseInOutQuad().setDelay(index * (0.3f / amountToDraw));


            ghostCards.Add(ghostCard);

        }

        for (int ii = cardsInHand.Count - 1; ii >= 0; ii -= 1)
        {
            VisibleCard ghostCard = Instantiate(MenuControl.Instance.visibleCardPrefab, visibleHandCardsHolder);
            ghostCard.RenderGhostCard();
            VisibleCard origVC = GetVisibleCardForCard(cardsInHand[cardsInHand.Count - 1 - ii]);
            ghostCard.transform.position = origVC.transform.position;
            ghostCard.transform.rotation = origVC.transform.rotation;
            ghostCard.transform.localScale = origVC.transform.localScale;

            Vector2 newPos = discardPileText.transform.position;

            LeanTween.move(ghostCard.gameObject, newPos, MenuControl.Instance.battleMenu.GetPlaySpeed()).setEaseInOutSine().setDelay(ii * (0.3f / cardsInHand.Count));
            LeanTween.rotateAround(ghostCard.gameObject, Vector3.forward, 720f, MenuControl.Instance.battleMenu.GetPlaySpeed()).setEaseInOutSine().setDelay(ii * (0.3f / cardsInHand.Count));

            ghostCard.GetComponent<CanvasGroup>().alpha = 1f;
            LeanTween.alphaCanvas(ghostCard.GetComponent<CanvasGroup>(), 0.1f, MenuControl.Instance.battleMenu.GetPlaySpeed() / 3f).setDelay((ii * (0.3f / cardsInHand.Count)) + (MenuControl.Instance.battleMenu.GetPlaySpeed() * 2f / 3f)).setEaseOutQuad();

            LeanTween.scale(ghostCard.gameObject, Vector3.one * 0.5f, MenuControl.Instance.battleMenu.GetPlaySpeed()).setEaseInOutSine().setDelay(ii * (0.3f / cardsInHand.Count));

            Destroy(ghostCard.gameObject, MenuControl.Instance.battleMenu.GetPlaySpeed() + (ii * (0.3f / cardsInHand.Count)));
        }

    }

    public void HideGhostHand()
    {
        foreach (VisibleCard ghostCard in ghostCards)
        {
            LeanTween.alphaCanvas(ghostCard.GetComponent<CanvasGroup>(), 0f, 0.3f);
            Destroy(ghostCard.gameObject, 0.3f);
        }
        ghostCards.Clear();
    }

    public List<Minion> GetMinionsOnBoard()
    {
        List<Minion> minions = new List<Minion>();
        foreach (Card card in cardsOnBoard)
        {
            if (card is Minion)
            {
                minions.Add((Minion)card);
            }
        }

        return minions;
    }

    public Hero CreateOrReplaceHeroWithTemplate(Hero newHeroTemplate, Tile tile, bool forceCreate = false)
    {
        if (GetHero() != null && !forceCreate)
        {

            Hero oldHero = GetHero();
            cardsOnBoard.Remove(oldHero);
            allCards.Remove(oldHero);
            Destroy(oldHero.gameObject);

        }

        Hero newHero = (Hero)CreateCardInGameFromTemplate(newHeroTemplate);
        cardsOnBoard.Add(newHero);

        newHero.InitializeUnit(false);
        newHero.MoveToTile(tile);

        foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
        {
            try
            {
                trigger.HeroSummoned(newHero);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }

        }

        return newHero;
    }
    public List<Card> GetItems()
    {
        List<Card> cards = new List<Card>();
        foreach (Card card in allCards)
        {
            if (card .isItem)
            {
                cards.Add(card);
            }
        }
        return cards;
    }
    public List<Card> GetArtifacts()
    {
        List<Card> cards = new List<Card>();
        foreach (Card card in allCards)
        {
            if (card is Artifact || card .isPotion)
            {
                cards.Add(card);
            }
        }
        return cards;
    }
}


public static class IListExtensions
{
    /// <summary>
    /// Shuffles the element order of the specified list.
    /// </summary>
    public static void Shuffle<T>(this IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
    public static void InsertAtRandom<T>(this IList<T> list, T value)
    {
        if (list == null)
            throw new ArgumentNullException(nameof(list));

        // 随机生成一个索引位置，包括最后一个位置
        int index = UnityEngine.Random.Range(0, list.Count + 1);

        // 在随机位置插入元素
        list.Insert(index, value);
    }

}