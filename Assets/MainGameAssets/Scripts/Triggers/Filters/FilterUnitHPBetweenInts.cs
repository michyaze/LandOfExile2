using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterUnitHPBetweenInts : TriggerFilter
{
    public int min = -1;
    public int max = -1;

    public override bool Check(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (targetTile == null || targetTile.GetUnit() == null) return false;

        if (min >= 0 && targetTile.GetUnit().currentHP < min) return false;

        if (max >= 0 && targetTile.GetUnit().currentHP > max) return false;

        return true;
    }
}
