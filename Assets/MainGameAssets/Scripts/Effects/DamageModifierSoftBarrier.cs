using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageModifierSoftBarrier : DamageModifier
{
    public override int ModifyAmount(Card sourceCard, Ability ability, Unit unit, int currentAmount)
    {
        if (unit == GetCard() && currentAmount > 0)
        {
            int origCharges = remainingCharges;
            if (remainingCharges == 1)
            {
                remainingCharges = 0;
            }
            else if (remainingCharges > 1)
            {
                ConsumeCharges(GetComponent<Ability>(), 1);
            }

            return currentAmount - origCharges;
        }
        return currentAmount;
    }
}
