using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectDamageHitAllUnitsSameRowAsTarget : Ability
{
    public int damageAmount;
    public bool useChargesAsDamageAmount;
    public bool notIncludingTarget;

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
            tile.GetUnit().SufferDamage(sourceCard, this, useChargesAsDamageAmount ? GetEffect().remainingCharges : damageAmount);
        }

        tile = targetTile.GetTileRight();
        if (tile != null && tile.GetUnit() != null)
        {
            tile.GetUnit().SufferDamage(sourceCard, this, useChargesAsDamageAmount ? GetEffect().remainingCharges : damageAmount);
        }

        if (targetTile.GetTileLeft() != null)
        {
            tile = targetTile.GetTileLeft().GetTileLeft();
            if (tile != null && tile.GetUnit() != null)
            {
                tile.GetUnit().SufferDamage(sourceCard, this, useChargesAsDamageAmount ? GetEffect().remainingCharges : damageAmount);
            }

            if (targetTile.GetTileLeft().GetTileLeft() != null)
            {
                tile = targetTile.GetTileLeft().GetTileLeft().GetTileLeft();
                if (tile != null && tile.GetUnit() != null)
                {
                    tile.GetUnit().SufferDamage(sourceCard, this, useChargesAsDamageAmount ? GetEffect().remainingCharges : damageAmount);
                }
            }
        }


        if (targetTile.GetTileRight() != null)
        {
            tile = targetTile.GetTileRight().GetTileRight();
            if (tile != null && tile.GetUnit() != null)
            {
                tile.GetUnit().SufferDamage(sourceCard, this, useChargesAsDamageAmount ? GetEffect().remainingCharges : damageAmount);
            }

            if (targetTile.GetTileRight().GetTileRight() != null)
            {
                tile = targetTile.GetTileRight().GetTileRight().GetTileRight();
                if (tile != null && tile.GetUnit() != null)
                {
                    tile.GetUnit().SufferDamage(sourceCard, this, useChargesAsDamageAmount ? GetEffect().remainingCharges : damageAmount);
                }
            }
        }
    }

}



