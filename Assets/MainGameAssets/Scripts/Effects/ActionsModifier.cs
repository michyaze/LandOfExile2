using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsModifier : Effect
{
    public virtual int ModifyAmount(Unit unit, int currentAmount)
    {
        return currentAmount;
    }
}
