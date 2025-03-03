using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceSantaTreasure : EventChoice
{
    public List<Card> treasures = new List<Card>();
    public override void PerformChoice()
    {

        List<Card> cardsToShow = new List<Card>();
        cardsToShow.AddRange(treasures);

        List<string> buttonLabels = new List<string>();
        buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Skip"));
        buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Confirm"));

        List<System.Action> actions = new List<System.Action>();
        actions.Add(() =>
        {
            CloseEvent();
        });
        actions.Add(() =>
        {
            MenuControl.Instance.heroMenu.AddCardToDeck(cardsToShow[MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts[0]]);
            MenuControl.Instance.dataControl.SaveData();
            CloseEvent();
        });

        MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("ChooseCardToAddPrompt"), 1, 1, true, -1, true);

    }
}
