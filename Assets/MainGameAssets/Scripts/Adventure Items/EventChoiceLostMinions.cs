using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceLostMinions : EventChoice
{

    public override void PerformChoice()
    {
        int seed = MenuControl.Instance.ApplySeed();
        Random.InitState(seed + MenuControl.Instance.adventureMenu.currentMapTileIndex);

        List<Card> cardsToShow = new List<Card>();

        List<Card> cards = MenuControl.Instance.heroMenu.FilterCardsOfLevel(MenuControl.Instance.heroMenu.heroPath.pathCards, 1);

        foreach (Card card in cards.ToArray())
        {
            if (!(card is Minion))
            {
                cards.Remove(card);
            }
        }

        while (cardsToShow.Count < 6)
        {
            Card newCard = cards[Random.Range(0, cards.Count)];
            if (!cardsToShow.Contains(newCard))
                cardsToShow.Add(newCard);
        }

        List<string> buttonLabels = new List<string>();
        buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Cancel"));
        buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Confirm"));

        List<System.Action> actions = new List<System.Action>();
        actions.Add(() => { });
        actions.Add(() =>
        {

            for (int ii = 0; ii < MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts.Count; ii += 1)
            {
                MenuControl.Instance.heroMenu.AddCardToDeck(MenuControl.Instance.cardChoiceMenu.cardsToShow[MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts[ii]]);
            }
            MenuControl.Instance.dataControl.SaveData();
            CloseEvent();
        });

        MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("LootPrompt"), 1, cardsToShow.Count, true, -1, false);

    }

}
