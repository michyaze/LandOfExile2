using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearEffects : Ability
{
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        base.PerformAbility(sourceCard, targetTile, amount);
        var unit = targetTile.GetUnit();
        if (unit)
        {
            unit.RemoveAllEffects(sourceCard,this);
        }
    }
}
