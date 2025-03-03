using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterEnemyUnitIsSource : TriggerFilter
{
    public bool minionOnly;
    public bool heroOnly;

    public override bool Check(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (minionOnly && !(sourceCard is Minion)) return false;

        if (heroOnly && !(sourceCard is Hero)) return false;

        if (targetTile != null && sourceCard.player != GetCard().player) return true;

        return false;
    }
}
