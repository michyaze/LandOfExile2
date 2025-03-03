using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPower : Ability
{
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        targetTile.GetUnit().ChangePower(this, targetTile.GetUnit().initialPower);
    }
}
