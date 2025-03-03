using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerModifierSelf : PowerModifier
{
    public int modifier;

    public override int ModifyPower(Unit unit, int currentPower, Unit attacker, Unit defender)
    {
        if (unit == GetCard() && unit.GetZone() == MenuControl.Instance.battleMenu.board)
        {
            return currentPower + modifier * ( GetComponent<Effect>().chargesStack? GetComponent<Effect>().remainingCharges:1);
        }

        return currentPower;
    }
}
