using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterUnitIsAdjacentToTargetTile : TriggerFilter
{
    public int range = 1;

    public override bool Check(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (targetTile != null && targetTile.GetAdjacentTilesLinear(range).Contains( ((Unit)sourceCard).GetTile() )) return true;

        return false;
    }
}
