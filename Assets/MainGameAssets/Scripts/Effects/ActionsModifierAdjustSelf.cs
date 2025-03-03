using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsModifierAdjustSelf : ActionsModifier
{
    public int amountToAdd;
    public bool useChargesAsWell;

    public override int ModifyAmount(Unit unit, int currentAmount)
    {

        if (unit == GetCard())
        {
            if (useChargesAsWell)
                return currentAmount + amountToAdd + remainingCharges;
            else return currentAmount + amountToAdd;
        }
          
        return currentAmount;
    }
}
