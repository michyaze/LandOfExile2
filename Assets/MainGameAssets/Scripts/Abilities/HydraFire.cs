using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydraFire : Ability
{
    public Minion headTemplate;
    public int damageAmount;
    //public Effect burnTemplate;
    //public int burnAmount = 3;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        Unit thisUnit = null;
        foreach (Tile tile2 in targetTile.GetAdjacentTilesLinear(1))
        {
            if (tile2.GetUnit() != null && tile2.GetUnit().cardTemplate.UniqueID == headTemplate.UniqueID)
            {
                thisUnit = tile2.GetUnit();
            }
        }


        int finalDamage = damageAmount;
        if (amount > 0) finalDamage = amount;


        int range = MenuControl.Instance.battleMenu.boardMenu.totalActiveTiles / 4;
        for (int ii = 1; ii <= range; ii += 1)
        {
            if (targetTile == thisUnit.GetTile().GetTileLeft(ii))
            {
                for (int xx = 1; xx <= range; xx += 1)
                {
                    Tile tile = thisUnit.GetTile().GetTileLeft(xx);
                    if (tile != null && tile.GetUnit() != null)
                    {

                        //tile.GetUnit().ApplyEffect(thisUnit, this, burnTemplate, burnAmount);
                        tile.GetUnit().SufferDamage(sourceCard, this, finalDamage);
                    }
                }
                break;

            }
            else if (targetTile == thisUnit.GetTile().GetTileUp(ii))
            {
                for (int xx = 1; xx <= range; xx += 1)
                {
                    Tile tile = thisUnit.GetTile().GetTileUp(xx);
                    if (tile != null && tile.GetUnit() != null)
                    {

                        //tile.GetUnit().ApplyEffect(thisUnit, this, burnTemplate, burnAmount);
                        tile.GetUnit().SufferDamage(sourceCard, this, finalDamage);
                    }
                }
                break;
            }
            else if (targetTile == thisUnit.GetTile().GetTileRight(ii))
            {
                for (int xx = 1; xx <= range; xx += 1)
                {
                    Tile tile = thisUnit.GetTile().GetTileRight(xx);
                    if (tile != null && tile.GetUnit() != null)
                    {

                        //tile.GetUnit().ApplyEffect(thisUnit, this, burnTemplate, burnAmount);
                        tile.GetUnit().SufferDamage(sourceCard, this, finalDamage);
                    }
                }
                break;
            }
            else if (targetTile == thisUnit.GetTile().GetTileDown(ii))
            {
                for (int xx = 1; xx <= range; xx += 1)
                {
                    Tile tile = thisUnit.GetTile().GetTileDown(xx);
                    if (tile != null && tile.GetUnit() != null)
                    {
                        //tile.GetUnit().ApplyEffect(thisUnit, this, burnTemplate, burnAmount);
                        tile.GetUnit().SufferDamage(sourceCard, this, finalDamage);

                    }
                }
                break;
            }
        }

    }
}
