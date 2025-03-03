using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerModifierMyHero : PowerModifier
{
    public int modifier;

    public override int ModifyPower(Unit unit, int currentPower, Unit attacker, Unit defender)
    {
        if (unit == GetCard().player.GetHero())
        {
            return currentPower + modifier;
        }

        return currentPower;
    }
}
