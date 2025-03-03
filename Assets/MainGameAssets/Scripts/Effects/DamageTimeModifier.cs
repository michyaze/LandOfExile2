using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTimeModifier : Effect
{
    public virtual int ModifyAmount(Card sourceCard, Ability ability, Unit unit, int currentAmount)
    {
        return 1;
    }
}
