using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterThisUnitIsSource : TriggerFilter
{
  
    public override bool Check(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (sourceCard == GetCard()) return true;

        return false;
    }
}
