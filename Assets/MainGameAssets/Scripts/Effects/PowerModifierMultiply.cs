using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerModifierMultiply : PowerModifier
{
    public float multiplier = 2f;
    public bool mustBeOneIfAbove1;

    public override int ModifyPower(Unit unit, int currentPower, Unit attacker, Unit defender)
    {
        int origPower = currentPower;
        int newPower = currentPower;

        if (unit == GetCard() && unit.GetZone() == MenuControl.Instance.battleMenu.board)
            newPower = Mathf.FloorToInt(currentPower * multiplier);

        if (mustBeOneIfAbove1 && origPower > 0) newPower = Mathf.Max(1, newPower);

        return newPower;
    }
}
