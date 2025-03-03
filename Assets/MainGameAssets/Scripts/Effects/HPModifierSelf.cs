using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPModifierSelf : HPModifier
{
    public int modifier;

    public override int ModifyAmount(Unit unit, int currentAmount)
    {
        if (unit == GetCard())
        {
            return currentAmount + modifier * (GetComponent<Effect>().chargesStack? GetComponent<Effect>().remainingCharges:1);
        }
        return currentAmount;
    }
}
