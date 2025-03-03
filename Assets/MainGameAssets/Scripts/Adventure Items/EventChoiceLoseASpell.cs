using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceLoseASpell : EventChoice
{

    public override void PerformChoice()
    {

        List<Card> cardsToShow = new List<Card>();

        foreach (Card card in MenuControl.Instance.heroMenu.cardsOwned)
        {
            if (card.cardTags.Contains(MenuControl.Instance.spellTag))
            {
                cardsToShow.Add(card);
            }
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
            MenuControl.Instance.dataControl.SaveData();
            CloseEvent();
        });

        MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("CardRemovalChoicePrompt"), 1, 1, true, -1, false);

    }

}
