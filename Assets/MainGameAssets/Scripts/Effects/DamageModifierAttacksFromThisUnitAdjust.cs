using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageModifierAttacksFromThisUnitAdjust : DamageModifier
{
    public float multiplier = 1;
    public int amountToAdjust = 0;

    public override int ModifyAmount(Card sourceCard, Ability ability, Unit unit, int currentAmount)
    {
        if (((Unit)GetCard()).GetZone() == MenuControl.Instance.battleMenu.board && ability.GetCard() == GetCard() && ability is Attack)
        {
            return Mathf.FloorToInt((currentAmount * multiplier) + amountToAdjust);
        }

        return currentAmount;
    }


}
