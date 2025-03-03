using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageModifierImmuneToMinions : DamageModifier
{


    public override int ModifyAmount(Card sourceCard, Ability ability, Unit unit, int currentAmount)
    {
        if (unit == GetCard())
        {
            if (ability.GetCard() is Minion || sourceCard is Minion)
                return -99999;

        }

        return currentAmount;
    }


}
