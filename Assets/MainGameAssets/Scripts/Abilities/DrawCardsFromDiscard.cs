using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCardsFromDiscard : Ability
{
    public int cardsToDraw = 1;
    public bool autoDrawFromLast;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (autoDrawFromLast)
        {
            for (int ii = 0; ii < cardsToDraw; ii += 1)
            {
                if (GetCard().player.cardsInDiscard.Count > 0)
                {
                    GetCard().player.cardsInDiscard[GetCard().player.cardsInDiscard.Count - 1].DrawThisCard();
                }
            }
        }
        else
        {

            List<Card> cardsToShow = new List<Card>();
            cardsToShow.AddRange(sourceCard.player.cardsInDiscard);

            if (cardsToShow.Count == 0) return;

            List<string> buttonLabels = new List<string>();
            buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Confirm"));

            List<System.Action> actions = new List<System.Action>();
            actions.Add(() =>
            {

                for (int ii = 0; ii < MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts.Count; ii += 1)
                {
                    Card card = MenuControl.Instance.cardChoiceMenu.cardsToShow[MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts[ii]];
                    card.DrawThisCard();
                }

                MenuControl.Instance.battleMenu.ProcessTriggeredAbilities();

            });

            MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("SelectCardsToDraw"), 1, 1, false, -1, false);
        }
    }
}
