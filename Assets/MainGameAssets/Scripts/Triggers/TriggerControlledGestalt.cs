using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerControlledGestalt : Trigger
{
    public override void TurnStarted(Player player)
    {
        if (player == GetCard().player && GetCard().GetZone() == MenuControl.Instance.battleMenu.board)
        {
            Unit unit = (Unit)GetCard();

            unit.ChangeCurrentHP(this, unit.currentHP - (unit.player.cardsOnBoard.Count - 1));

            foreach (Unit otherUnit in unit.player.cardsOnBoard)
            {
                if (otherUnit != unit)
                {
                    otherUnit.ChangePower(this, otherUnit.currentPower + 1);
                }
            }
        }
    }
}
