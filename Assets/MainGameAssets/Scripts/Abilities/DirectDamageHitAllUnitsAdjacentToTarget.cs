using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectDamageHitAllUnitsAdjacentToTarget : Ability
{
    public int damageAmount;
    public bool useChargesAsDamageAmount;
    public bool notIncludingTarget;
    public bool minionsOnly;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        if (!notIncludingTarget)
        {
            if (targetTile.GetUnit() != null)
                targetTile.GetUnit().SufferDamage(sourceCard, this, useChargesAsDamageAmount ? GetEffect().remainingCharges : damageAmount);
        }


        Tile tile = targetTile.GetTileLeft();
        if (tile != null && tile.GetUnit() != null)
        {
            if (!minionsOnly || tile.GetUnit() is Minion)
            {
                tile.GetUnit().SufferDamage(sourceCard, this, useChargesAsDamageAmount ? GetEffect().remainingCharges : damageAmount);
            }
        }

        tile = targetTile.GetTileRight();
        if (tile != null && tile.GetUnit() != null)
        {
            if (!minionsOnly || tile.GetUnit() is Minion)
            {
                tile.GetUnit().SufferDamage(sourceCard, this, useChargesAsDamageAmount ? GetEffect().remainingCharges : damageAmount);
            }
        }

        tile = targetTile.GetTileUp();
        if (tile != null && tile.GetUnit() != null)
        {
            if (!minionsOnly || tile.GetUnit() is Minion)
            {
                tile.GetUnit().SufferDamage(sourceCard, this, useChargesAsDamageAmount ? GetEffect().remainingCharges : damageAmount);
            }
        }

        tile = targetTile.GetTileDown();
        if (tile != null && tile.GetUnit() != null)
        {
            if (!minionsOnly || tile.GetUnit() is Minion)
            {
                tile.GetUnit().SufferDamage(sourceCard, this, useChargesAsDamageAmount ? GetEffect().remainingCharges : damageAmount);
            }
        }

    }

}



