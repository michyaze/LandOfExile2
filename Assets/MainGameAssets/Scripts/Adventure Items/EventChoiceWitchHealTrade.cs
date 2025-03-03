using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceWitchHealTrade : EventChoice
{

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

                MenuControl.Instance.heroMenu.hero.Heal(null, null, 999999);

                MenuControl.Instance.dataControl.SaveData();
                MenuControl.Instance.adventureMenu.RenderScreen();
                CloseEvent();
            });

            MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("ChooseACardToTrade"), 1, 1, true, -1, false);

        }
        else
        {
            Card card = null;
            MenuControl.Instance.cardChoiceMenu.ShowNotifcation(card, () => { }, MenuControl.Instance.GetLocalizedString("You lack treasure cards to pay with."));
        }

    }
}
