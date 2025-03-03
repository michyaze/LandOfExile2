using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterEnemyUnitIsOnTile : TriggerFilter
{
    public bool minionOnly;

    public override bool Check(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (minionOnly && !(targetTile.GetUnit() is Minion)) return false;

        if (targetTile != null && sourceCard.player != targetTile.GetUnit().player) return true;

        return false;
    }
}
