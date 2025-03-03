using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveThisUnitToTile : Ability
{
    public bool noMoveTrigger;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        Unit unit = ((Unit)GetCard());
        if (targetTile.isMoveable() || targetTile.GetUnit() == unit)
        {
            if (noMoveTrigger)
            {
                unit.MoveToTile(targetTile);
            }
            else
            {
                unit.TargetTile(targetTile, false);
            }
        }
    }
}
