using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMovementBack : Ability
{

    public TargetValidator movementType;

    public bool ignoreLargeHero;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        
        var unit = targetTile.GetUnit();
        if (ignoreLargeHero && unit is LargeHero)
        {
            return;
        }

        
        targetTile.GetUnit().ChangeMovementBack();
    }
}
