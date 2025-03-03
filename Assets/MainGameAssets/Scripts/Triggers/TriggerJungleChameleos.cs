using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerJungleChameleos : Trigger
{
    public override void CardPlayed(Card card, Tile tile, List<Tile> tiles)
    {
        if (card.UniqueID == "Green01" && GetCard().GetZone() == MenuControl.Instance.battleMenu.board)
        {
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                List<Minion> minionsInHand = new List<Minion>();
                foreach (Card card1 in GetCard().player.cardsInHand)
                {
                    if (card1 is Minion)
                    {
                        minionsInHand.Add((Minion)card1);
                    }
                }

                if (minionsInHand.Count > 0)
                {
                    bool isSummoningSick = ((Unit)GetCard()).GetEffectsOfType<SummoningSick>().Count > 0;
                    Minion newMinion = (Minion)GetCard().player.CreateCardInGameFromTemplate(minionsInHand[Random.Range(0, minionsInHand.Count)].cardTemplate);

                    int oldMoves = ((Unit)GetCard()).remainingMoves;
                    int oldActions = ((Unit)GetCard()).remainingActions;

                    Tile oldTile = ((Unit)GetCard()).GetTile();
                    ((Unit)GetCard()).PutIntoZone(MenuControl.Instance.battleMenu.removedFromGame);

                    newMinion.TargetTile(oldTile, false);
                    if (!isSummoningSick)
                        newMinion.RemoveEffect(GetCard(), this, newMinion.GetEffectsOfType<SummoningSick>()[0]);
                    newMinion.remainingActions = oldActions;
                    newMinion.remainingMoves = oldMoves;

                }

            });
        }

    }
}
