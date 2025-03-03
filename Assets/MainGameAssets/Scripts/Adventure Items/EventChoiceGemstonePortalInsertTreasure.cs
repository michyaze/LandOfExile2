using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceGemstonePortalInsertTreasure : EventChoice
{
    public EventDefinition nextEventDefinition;

    public override void PerformChoice()
    {

        List<Card> cardsToShow = new List<Card>();
        foreach (Card card in MenuControl.Instance.heroMenu.GetTreasureCardsOwned())
        {
           // if (card.cardTags.Contains(MenuControl.Instance.treasureTag))
            {
                cardsToShow.Add(card);
            }
        }
        if (cardsToShow.Count > 0)
        {

            List<string> buttonLabels = new List<string>();
            buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Cancel"));
            buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Confirm"));

            List<System.Action> actions = new List<System.Action>();
            actions.Add(() => { });
            actions.Add(() =>
            {

                for (int ii = 0; ii < MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts.Count; ii += 1)
                {
                    MenuControl.Instance.heroMenu.RemoveCardFromDeck(MenuControl.Instance.cardChoiceMenu.cardsToShow[MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts[ii]]);

                }
                MenuControl.Instance.dataControl.SaveData();
                MenuControl.Instance.eventMenu.ShowEvent(nextEventDefinition);
            });

            MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("Pay 1 treasure"), 1, 1, true, -1, false);

        }
        else
        {

            List<string> buttonLabels = new List<string>();
            buttonLabels.Add(MenuControl.Instance.GetLocalizedString("OK"));

            List<System.Action> actions = new List<System.Action>();
            actions.Add(() => { });

            MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("You lack treasure cards to pay with."), 0, 0, true, -1, false);
        }

    }
}
