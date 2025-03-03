using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterAmountIsBetweenInts : TriggerFilter
{
    public int min = -1;
    public int max = -1;

    public override bool Check(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (min >= 0 && amount < min) return false;

        if (max >= 0 && amount > max) return false;

        return true; 
    }
}
