using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceFellowAdventurerTrade : EventChoice
{
    public override void PerformChoice()
    {

        List<Card> cardsToShow = new List<Card>();

        foreach (Card card in MenuControl.Instance.heroMenu.cardsOwned)
        {
            if (!(card is Hero) && card.level >= 1)
            {
                cardsToShow.Add(card);
            }
        }

        List<string> buttonLabels = new List<string>();
        buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Cancel"));
        buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Confirm"));

        List<System.Action> actions = new List<System.Action>();
        actions.Add(() => { });
        actions.Add(() =>
        {
            Card oldCard = null;
           
            oldCard = MenuControl.Instance.cardChoiceMenu.cardsToShow[MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts[0]];
            MenuControl.Instance.heroMenu.RemoveCardFromDeck(oldCard);

            List<Card> cards = MenuControl.Instance.heroMenu.FilterCardsOfLevel(MenuControl.Instance.heroMenu.heroClass.classCards, oldCard.level);
            cards.AddRange(MenuControl.Instance.heroMenu.FilterCardsOfLevel(MenuControl.Instance.heroMenu.heroPath.pathCards, oldCard.level));

            Card newCard = cards[Random.Range(0, cards.Count)];

            MenuControl.Instance.heroMenu.AddCardToDeck(newCard);


            MenuControl.Instance.cardChoiceMenu.ShowNotifcation(newCard, () =>
            {
                CloseEvent();
            }, MenuControl.Instance.GetLocalizedString("CardAddedToDeckPrompt", "This card has been added to your deck:"));

            
        });

        MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("ChooseACardToTrade"), 1, 1, true, -1, false);

    }
}
