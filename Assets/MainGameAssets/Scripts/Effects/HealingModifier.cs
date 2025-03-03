using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingModifier : Effect
{
    public virtual int ModifyAmount(Unit unit, int currentAmount)
    {
        return currentAmount;
    }
}
