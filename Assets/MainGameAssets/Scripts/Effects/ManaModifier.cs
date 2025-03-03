using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaModifier : Effect
{
    public virtual int ModifyAmount(Player unit, int currentAmount)
    {
        return currentAmount;
    }
}
