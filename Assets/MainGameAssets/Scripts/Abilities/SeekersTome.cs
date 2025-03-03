using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekersTome : Ability
{
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        List<Card> cardsToShow = new List<Card>();
        cardsToShow.AddRange(sourceCard.player.cardsInDeck);
        cardsToShow.AddRange(sourceCard.player.cardsInDiscard);

        if (cardsToShow.Count == 0) return;

        List<string> buttonLabels = new List<string>();
        buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Confirm"));

        List<System.Action> actions = new List<System.Action>();
        actions.Add(() => {

            for (int ii = 0; ii < MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts.Count; ii += 1)
            {
                MenuControl.Instance.cardChoiceMenu.cardsToShow[MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts[ii]].DrawThisCard();
            }

            MenuControl.Instance.battleMenu.ProcessTriggeredAbilities();

        });

        MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("SelectCardsToDraw"), 1, 1, false, -1, false);

    }
}
