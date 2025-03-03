using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMovement : Ability
{

    public TargetValidator movementType;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        targetTile.GetUnit().ChangeMovementTemporarily(movementType) ;
    }
}
