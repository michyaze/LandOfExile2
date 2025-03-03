using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummoningModifier : Trigger
{
    public virtual bool CanSummon(Unit unit, Tile tile)
    {
        return true;
    }
}
