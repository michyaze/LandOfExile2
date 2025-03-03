using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerModifierIncreaseIfLastMinion : PowerModifier
{
    public int modifier = 3;
    public override int ModifyPower(Unit unit, int currentPower, Unit attacker, Unit defender)
    {
        if (GetCard() == unit && unit.GetZone() == MenuControl.Instance.battleMenu.board && unit.player.GetMinionsOnBoard().Count == 1 && unit.player.GetOpponent().GetMinionsOnBoard().Count == 0)
        {
            return currentPower + modifier;
        }

        return currentPower;
    }
}
