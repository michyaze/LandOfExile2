using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArcaneLock : Trigger
{
    public List<Card> nonDisardedCardsThisTurn = new List<Card>();
    public int cardsToKeep;

    public override void BeforeEndTurn(Player player)
    {
        nonDisardedCardsThisTurn.Clear();
        if (player == GetCard().player)
        {
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
                            nonDisardedCardsThisTurn.Add(MenuControl.Instance.cardChoiceMenu.cardsToShow[MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts[ii]]);
                        }
                        MenuControl.Instance.battleMenu.afterProcessingTriggeredAbilities = MenuControl.Instance.battleMenu.EndTurn;
                        MenuControl.Instance.battleMenu.ProcessTriggeredAbilities(true);

                    });

                    MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("ArcaneLockPrompt").Replace("XX", cardsToKeep.ToString()), 0, cardsToKeep, false, -1, false);



                });

            }
        }
    }

    public override void TurnEnded(Player player)
    {
        if (player == GetCard().player)
        {
            nonDisardedCardsThisTurn.Clear();
        }
    }

    public override bool CanDiscard(Card card, bool autoEndTurn)
    {
        return !nonDisardedCardsThisTurn.Contains(card);
    }
}
