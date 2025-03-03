using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterThisUnitIsTarget : TriggerFilter
{
  
    public override bool Check(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (targetTile == ((Unit)GetCard()).GetTile()) return true;

        if (GetCard() is LargeHero && ((LargeHero)GetCard()).GetTiles().Contains(targetTile)) return true;

        return false;
    }
}
