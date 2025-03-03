using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerModifierIncreaseByMissingHP : PowerModifier
{
    public override int ModifyPower(Unit unit, int currentPower, Unit attacker, Unit defender)
    {
        int newPower = currentPower;

        if (unit == GetCard() && unit.GetZone() == MenuControl.Instance.battleMenu.board)
        {
            var currentHP = unit.GetHP();
            currentHP = Math.Max(0, currentHP);
            newPower = currentPower + unit.GetInitialHP() - currentHP;
        }

        return newPower;
    }
}
