using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCardShuffle : Trigger
{

    public override void TurnStarted(Player player)
    {
        if (player == GetCard().player)
        {
            int cardsToDiscard = GetEffect().remainingCharges;

            List<Card> cardsToShow = new List<Card>();
            cardsToShow.AddRange(GetCard().player.cardsInHand);

            if (cardsToShow.Count == 0) return;

            List<string> buttonLabels = new List<string>();
            buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Cancel"));
            buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Confirm"));

            List<System.Action> actions = new List<System.Action>();
            actions.Add(() => { });
            actions.Add(() =>
            {

                for (int ii = 0; ii < MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts.Count; ii += 1)
                {
                    Card card = MenuControl.Instance.cardChoiceMenu.cardsToShow[MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts[ii]];
                    card.DiscardThisCard();
                }

                for (int ii = 0; ii < cardsToDiscard; ii += 1)
                {
                    GetCard().player.DrawACard();
                }

                MenuControl.Instance.battleMenu.ProcessTriggeredAbilities();

            });

            MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("SelectCardsToRedraw").Replace("XX", Mathf.Min(cardsToShow.Count, cardsToDiscard).ToString()), 1, Mathf.Min(cardsToShow.Count, cardsToDiscard), true, -1, false);
        }
    }
}
