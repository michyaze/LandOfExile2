using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DamageModifierReduce : DamageModifier
{
    public int reduceAmount = 1;
    public bool multiplyCharges = true;
    public override int ModifyAmount(Card sourceCard, Ability ability, Unit unit, int currentAmount)
    {
        if (unit == GetCard())
        {
            if (multiplyCharges)
            {
                return currentAmount - remainingCharges * reduceAmount;
            }
            else
            {
                return currentAmount - reduceAmount;
            }
        }
        return currentAmount;
    }
}
