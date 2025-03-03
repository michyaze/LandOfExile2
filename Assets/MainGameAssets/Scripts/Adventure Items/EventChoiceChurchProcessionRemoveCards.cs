using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceChurchProcessionRemoveCards : EventChoice
{
    public override void PerformChoice()
    {
        MenuControl.Instance.cardChoiceMenu.ShowPayTreasure(() =>
        {
            List<Card> cardsToShow = new List<Card>();

            foreach (Card card in MenuControl.Instance.heroMenu.cardsOwned)
            {
                if (!(card is Hero))
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
                    CloseEvent();
                });

                MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("ChoicePayChurchCardDescription"), 1, 2, true, -1, false);

            }

        });
    }

}
