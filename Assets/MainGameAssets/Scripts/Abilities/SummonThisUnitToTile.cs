using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonThisUnitToTile : Ability
{

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (targetTile.isMoveable())
        {
            GetCard().TargetTile(targetTile, false);
       
        }

    }
}
