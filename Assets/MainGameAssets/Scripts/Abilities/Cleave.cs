using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cleave : Ability
{
    public bool includeDiagonals;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        Unit thisUnit = (Unit)sourceCard;

        int damageAmount = ((Unit)sourceCard).GetPower((Unit)sourceCard, targetTile == null || targetTile.GetUnit() == null ? null : targetTile.GetUnit());

        

        foreach (Tile tile in thisUnit.GetAdjacentTiles())
        {
            if (tile != null && tile != targetTile && (targetTile == null || targetTile.GetUnit() != tile.GetUnit()) && tile.GetUnit() != null && tile.GetUnit().player != sourceCard.player)
            {
                if (thisUnit.GetComponent<Attack>() != null)
                {

                    if (thisUnit.checkCanAttackTarget(tile.GetUnit(),
                            thisUnit.GetComponent<Attack>().GetTargetValidator()))
                    {
                        
                        tile.GetUnit().SufferDamage(sourceCard, this, damageAmount);
                    }
                }
                else
                {
                    Debug.LogError(("No attack component on " + thisUnit.name));
                    tile.GetUnit().SufferDamage(sourceCard, this, damageAmount);
                }
            }
        }

        if (includeDiagonals)
        {
            foreach (Tile tile in thisUnit.GetDiagonalTiles())
            {
                if (tile != null && tile != targetTile && tile.GetUnit() != null && tile.GetUnit().player != sourceCard.player)
                {
                    tile.GetUnit().SufferDamage(sourceCard, this, damageAmount);
                }
            }
        }

    }

}
