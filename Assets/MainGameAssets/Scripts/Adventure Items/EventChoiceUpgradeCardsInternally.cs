using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceUpgradeCardsInternally : EventChoice
{
    public int changeHP;

    public void FinishEvent()
    {
        if (changeHP > 0)
        {
            MenuControl.Instance.heroMenu.hero.Heal(null, null, changeHP);
        }
        else if (changeHP < 0)
        {
            MenuControl.Instance.heroMenu.hero.currentHP += changeHP;
        }

        
            CloseEvent();
                
            MenuControl.Instance.dataControl.SaveData();
                
            MenuControl.Instance.adventureMenu.ContinueAdventure();
    }
    public override void PerformChoice()
    {
        if (changeHP < 0 && -changeHP >= MenuControl.Instance.heroMenu.hero.currentHP)
        {
            MenuControl.Instance.cardChoiceMenu.ShowNotifcation(null, ()=>
            {
                // CloseEvent();
                //
                // MenuControl.Instance.dataControl.SaveData();
                //
                // MenuControl.Instance.adventureMenu.ContinueAdventure();
            }, MenuControl.Instance.GetLocalizedString("NotEnoughHPToUpgradePrompt"),false);
            return;
        }
        
        
        List<Card> cardsToShow = new List<Card>();

        foreach (Card card in MenuControl.Instance.heroMenu.cardsOwned)
        {
            if (card.RandomUpgradeCard!=null)
            {
                cardsToShow.Add(card);
            }
        }
        List<string> buttonLabels = new List<string>();
        buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Cancel"));
        List<System.Action> actions = new List<System.Action>();
        actions.Add(() => { });
        if (cardsToShow.Count == 0)
        {
            
            MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("ChooseACardToUpgradeNone"), 1, 1, true, -1, false);
            return;
        }

        
        buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Confirm"));

        actions.Add(() =>
        {
            Card oldCard = MenuControl.Instance.cardChoiceMenu.cardsToShow[MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts[0]];
            if (oldCard.upgradeCards.Count > 1)
            {
                MenuControl.Instance.upgradeSelectCardView.ShowMenuOutOfShop(oldCard, FinishEvent);
            }
            else
            {
                var newcard = MenuControl.Instance.heroMenu.UpgradeToRandomCardInDeck(oldCard);
                MenuControl.Instance.cardChoiceMenu.ShowNotifcation(newcard, FinishEvent, MenuControl.Instance.GetLocalizedString("CardsUpgradedInDeckPrompt"),true);
            }
            
            // Card oldCard = null;
            //
            // oldCard = MenuControl.Instance.cardChoiceMenu.cardsToShow[MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts[0]];
            // MenuControl.Instance.heroMenu.RemoveCardFromDeck(oldCard);
            //
            // List<Card> cards = MenuControl.Instance.heroMenu.FilterCardsOfLevel(MenuControl.Instance.heroMenu.heroClass.classCards, oldCard.level);
            // cards.AddRange(MenuControl.Instance.heroMenu.FilterCardsOfLevel(MenuControl.Instance.heroMenu.heroPath.pathCards, oldCard.level));
            //
            // Card newCard = cards[Random.Range(0, cards.Count)];
            //
            // MenuControl.Instance.heroMenu.AddCardToDeck(newCard);


            
        });

        MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("ChooseACardToUpgrade"), 1, 1, true, -1, false);

    }
}
