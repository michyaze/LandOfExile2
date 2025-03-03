using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingModifierMultiply : HealingModifier
{
    public float multiplier = 2f;

    public override int ModifyAmount(Unit unit, int currentAmount)
    {
        int newPower = currentAmount;
        if (unit == GetCard())
            newPower = Mathf.FloorToInt(currentAmount * multiplier);

        return newPower;
    }
}
