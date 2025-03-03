using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerJungleShifter : Trigger
{
    public override void CardPlayed(Card card, Tile tile, List<Tile> tiles)
    {
        if (card.UniqueID == "Green01" && GetCard().GetZone() == MenuControl.Instance.battleMenu.board)
        {
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                Unit thisUnit = (Unit)GetCard();
                List<Minion> adjacentMinions = new List<Minion>();
                foreach (Tile tile1 in thisUnit.GetAdjacentTiles())
                {
                    if (tile1.GetUnit() is Minion)
                    {
                        adjacentMinions.Add((Minion)tile1.GetUnit());
                    }
                }
                if (adjacentMinions.Count > 0)
                {
                    Minion minionToCopy = adjacentMinions[Random.Range(0, adjacentMinions.Count)];
                    Minion newMinion = (Minion)GetCard().player.CreateCardInGameFromTemplate(minionToCopy.cardTemplate);

                    foreach (Effect effect in minionToCopy.currentEffects)
                    {
                        newMinion.ApplyEffect(GetCard(), this, effect.originalTemplate, effect.remainingCharges);

                    }

                    if (newMinion.GetEffectsWithTemplate(GetEffect().originalTemplate).Count == 0)
                    {
                        newMinion.ApplyEffect(GetCard(), this, GetEffect().originalTemplate, 0);
                    }

                    int oldMoves = thisUnit.remainingMoves;
                    int oldActions = thisUnit.remainingActions;

                    Tile oldTile = thisUnit.GetTile();
                    thisUnit.PutIntoZone(MenuControl.Instance.battleMenu.removedFromGame);

                    //newMinion.TargetTile(oldTile, false);
                    //newMinion.RemoveEffect(GetCard(), this, newMinion.GetEffectsOfType<SummoningSick>()[0]);

                    newMinion.player.PutCardIntoZone(newMinion, MenuControl.Instance.battleMenu.board);

                    newMinion.InitializeUnit(true);

                    newMinion.ForceMove(oldTile);


                    newMinion.remainingActions = oldActions;
                    newMinion.remainingMoves = oldMoves;

                }

            });
        }
    }
}
