using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceLoseAnArtifact : EventChoice
{

    public override void PerformChoice()
    {
        Card crescentMoonKey = MenuControl.Instance.levelUpMenu.strangeKeyForFloor4;

        List<Card> cardsToShow = new List<Card>();

        foreach (Card card in MenuControl.Instance.heroMenu.artifactsOwned)
        {
            cardsToShow.Add(card);
        }
        Random.InitState(MenuControl.Instance.adventureMenu.currentMapTileIndex);
        cardsToShow.Shuffle();
        if (cardsToShow.Count>3)
            cardsToShow.RemoveRange(3, cardsToShow.Count - 3);

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

            MenuControl.Instance.heroMenu.AddCardToDeck(crescentMoonKey);

            MenuControl.Instance.dataControl.SaveData();
            CloseEvent();


            List<Card> cardsToShow2 = new List<Card>();
            cardsToShow2.Add(crescentMoonKey);

            List<string> buttonLabels2 = new List<string>();
            buttonLabels2.Add(MenuControl.Instance.GetLocalizedString("OK"));

            List<System.Action> actions2 = new List<System.Action>();
            actions2.Add(() => { });

            MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow2, buttonLabels2, actions2, MenuControl.Instance.GetLocalizedString("CardAddedToDeckPrompt"), 0, 0, true, -1, false);

        });

        MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("CardRemovalChoicePrompt"), 1, 1, true, -1, false);

    }

}
