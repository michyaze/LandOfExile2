using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovesModifierThisUnit : MovesModifier
{
    public int amountToAdd = 1;
    public bool useChargesAsAmount;
    public bool chargesAreNegative;

    public override int ModifyAmount(Unit unit, int currentAmount)
    {
        if (GetCard() == unit && GetCard().GetZone() == MenuControl.Instance.battleMenu.board)
        {
            if (useChargesAsAmount) return currentAmount + ((chargesAreNegative ? -1 : 1) * remainingCharges);

            return currentAmount + amountToAdd;
        }

        return currentAmount;
    }
}
