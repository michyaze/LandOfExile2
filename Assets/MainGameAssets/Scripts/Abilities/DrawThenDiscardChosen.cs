using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawThenDiscardChosen : Ability
{

    public int cardsToDraw;
    public int cardsToDiscard;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        for (int ii = 0; ii < cardsToDraw; ii += 1)
        {
            GetCard().player.DrawACard();
        }

        List<Card> cardsToShow = new List<Card>();
        cardsToShow.AddRange(sourceCard.player.cardsInHand);

        if (cardsToShow.Count == 0) return;

        List<string> buttonLabels = new List<string>();
        buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Confirm"));

        List<System.Action> actions = new List<System.Action>();
        actions.Add(() => {

            for (int ii = 0; ii < MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts.Count; ii += 1)
            {
                Card card = MenuControl.Instance.cardChoiceMenu.cardsToShow[MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts[ii]];
                card.DiscardThisCard();
            }

            MenuControl.Instance.battleMenu.ProcessTriggeredAbilities();

        });

        MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("SelectCardsToDiscardMin").Replace("XX", Mathf.Min(cardsToShow.Count, cardsToDiscard).ToString()), Mathf.Min(cardsToShow.Count,cardsToDiscard), Mathf.Min(cardsToShow.Count, cardsToDiscard), false, -1, false);
    }
}
