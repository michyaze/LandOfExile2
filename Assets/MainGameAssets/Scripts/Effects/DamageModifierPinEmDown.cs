using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageModifierPinEmDown : DamageModifier
{
    public int modifier = 2;

    public override int ModifyAmount(Card sourceCard, Ability ability, Unit unit, int currentAmount)
    {
        if (unit.player != GetCard().player)
        {
            if (unit.IsPinned())
            {
                return currentAmount + modifier;
            }
        }

        return currentAmount;
    }
}
