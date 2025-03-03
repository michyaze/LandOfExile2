using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageModifierBleed : DamageModifier
{

    public override int ModifyAmount(Card sourceCard, Ability ability, Unit unit, int currentAmount)
    {
        if (unit == GetCard())
        {
            if (ability is Attack)
                return currentAmount + remainingCharges;

        }

        return currentAmount;
    }


}
