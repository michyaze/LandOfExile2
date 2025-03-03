using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageModifierThisUnitMultiplier : DamageModifier
{
    public float multiplier = 2;

    public override int ModifyAmount(Card sourceCard, Ability ability, Unit unit, int currentAmount)
    {
        if (((Unit)GetCard()).GetZone() == MenuControl.Instance.battleMenu.board && unit == GetCard())
        {
            return Mathf.FloorToInt(currentAmount * multiplier);
        }

        return currentAmount;
    }


}
