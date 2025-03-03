using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerModifierIncreaseAllFriendlyUnitsInPlay : PowerModifier
{

    public int increaseAmount;

    public override int ModifyPower(Unit unit, int currentPower, Unit attacker, Unit defender)
    {
        if (GetCard().player == unit.player && unit.GetZone() == MenuControl.Instance.battleMenu.board && ((Unit)GetCard()).GetZone() == MenuControl.Instance.battleMenu.board)
        {
            return currentPower + increaseAmount;
        }

        return currentPower;
    }
}
