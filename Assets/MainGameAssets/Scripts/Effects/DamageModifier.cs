using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageModifier : Effect
{
    public virtual int ModifyAmount(Card sourceCard, Ability ability, Unit unit, int currentAmount)
    {
        return currentAmount;
    }
}
