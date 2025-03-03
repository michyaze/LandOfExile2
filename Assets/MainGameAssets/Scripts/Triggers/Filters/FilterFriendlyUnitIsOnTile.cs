using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterFriendlyUnitIsOnTile : TriggerFilter
{
    public bool otherUnit;
    public bool minionOnly;
    public bool heroOnly;

    public override bool Check(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (targetTile != null && sourceCard.player == targetTile.GetUnit().player && 
            ((otherUnit && targetTile.GetUnit() != sourceCard) || !otherUnit) && (!minionOnly || targetTile.GetUnit() is Minion) && (!heroOnly || targetTile.GetUnit() is Hero)) return true;

        return false;
    }
}
