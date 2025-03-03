using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  MovesModifier : Effect
{
    public virtual int ModifyAmount(Unit unit, int currentAmount)
    {
        return currentAmount;
    }
}
