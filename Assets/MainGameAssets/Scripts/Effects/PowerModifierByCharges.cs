using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerModifierByCharges : PowerModifier
{
    public bool reduceByCharges;

    public override int ModifyPower(Unit unit, int currentPower, Unit attacker, Unit defender)
    {
        int newPower = currentPower;
        if (unit == GetCard() && unit.GetZone() == MenuControl.Instance.battleMenu.board)
            newPower = Mathf.Max(0, currentPower + (reduceByCharges ? -remainingCharges : remainingCharges));

        return newPower;
    }
}
