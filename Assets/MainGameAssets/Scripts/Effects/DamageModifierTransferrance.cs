using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageModifierTransferrance : DamageModifier
{

    public override int ModifyAmount(Card sourceCard, Ability ability, Unit unit, int currentAmount)
    {
        //if (unit == GetCard().player.GetHero())
        //{
        //    return Mathf.Max(0, currentAmount - remainingCharges);
        //}

        //No longer changes damage
        return currentAmount;
    }
}
