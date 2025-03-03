using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRetainedStrength : Trigger
{
    public List<Card> cardsNotToDiscard = new List<Card>();

    public override void BeforeEndTurn(Player player)
    {
        if (player == GetCard().player)
        {
            cardsNotToDiscard.Clear();
            if (player.cardsInHand.Count > 0)
            {
                MenuControl.Instance.battleMenu.afterProcessingTriggeredAbilities = null;

                MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                {

                    List<Card> cardsToShow = new List<Card>();
                    cardsToShow.AddRange(GetCard().player.cardsInHand);

                    if (cardsToShow.Count == 0) return;

                    List<string> buttonLabels = new List<string>();
                    buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Confirm"));

                    List<System.Action> actions = new List<System.Action>();
                    actions.Add(() => {

                        for (int ii = 0; ii < MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts.Count; ii += 1)
                        {
                            cardsNotToDiscard.Add(MenuControl.Instance.cardChoiceMenu.cardsToShow[MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts[ii]]);
                        }
                        MenuControl.Instance.battleMenu.afterProcessingTriggeredAbilities = MenuControl.Instance.battleMenu.EndTurn;
                        MenuControl.Instance.battleMenu.ProcessTriggeredAbilities(true);

                    });

                    MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("ArcaneLockPrompt").Replace("XX", GetEffect().remainingCharges.ToString()), 0, GetEffect().remainingCharges, false, -1, false);

                });

            }
        }
    }
    public override void AfterTurnEnded(Player player)
    {
        if (player == GetCard().player)
        {
            cardsNotToDiscard.Clear();
        }
    }

    public override bool CanDiscard(Card card, bool autoEndTurn)
    {
        return !cardsNotToDiscard.Contains(card);
    }
}
