using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyUnit : Ability
{
    public bool andExhaust;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (targetTile == null || targetTile.GetUnit() == null)
        {
            return;
        }
        if (targetTile.GetUnit() != null)
        {
            Unit unit = targetTile.GetUnit();
            unit.SufferDamage(sourceCard, this, 0, true);

            if (andExhaust)
            {
                unit.ExhaustThisCard();
            }
        }
    }
}
