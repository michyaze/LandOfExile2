using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostsModifier : Effect
{
    public virtual int ModifyAmount(Card card, int currentAmount)
    {
        return currentAmount;
    }
}
