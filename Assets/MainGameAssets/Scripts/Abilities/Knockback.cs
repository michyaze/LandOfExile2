using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : Ability
{
    public int knockbackAmount = 1;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        int totalDamage = 0;

        Unit thisUnit = sourceCard.player.GetHero();
        if (sourceCard is Unit)
            thisUnit = (Unit)sourceCard;

        Unit targetUnit = targetTile.GetUnit();
        if (targetUnit == null) return;

        if (!(targetUnit is LargeHero))
        {
            for (int ii = 1; ii <= 4; ii += 1)
            {
                if (thisUnit.GetTilesLeft(ii).Contains(targetTile))
                {
                    for (int xx = 1; xx <= knockbackAmount; xx += 1)
                    {
                        Tile tile = targetTile.GetTileLeft(xx);
                        if (tile != null && tile.isMoveable())
                        {
                            targetUnit.ForceMove(tile);
                        }
                        else
                        {
                            totalDamage += knockbackAmount - (xx - 1);
                            break;
                        }
                    }
                    break;

                }
                else if (thisUnit.GetTilesUp(ii).Contains(targetTile))
                {
                    for (int xx = 1; xx <= knockbackAmount; xx += 1)
                    {
                        Tile tile = targetTile.GetTileUp(xx);
                        if (tile != null && tile.isMoveable())
                        {
                            targetUnit.ForceMove(tile);
                        }
                        else
                        {
                            totalDamage += knockbackAmount - (xx - 1);
                            break;
                        }
                    }
                    break;
                }
                else if (thisUnit.GetTilesRight(ii).Contains(targetTile))
                {
                    for (int xx = 1; xx <= knockbackAmount; xx += 1)
                    {
                        Tile tile = targetTile.GetTileRight(xx);
                        if (tile != null && tile.isMoveable())
                        {
                            targetUnit.ForceMove(tile);
                        }
                        else
                        {
                            totalDamage += knockbackAmount - (xx - 1);
                            break;
                        }
                    }
                    break;
                }
                else if (thisUnit.GetTilesDown(ii).Contains(targetTile))
                {
                    for (int xx = 1; xx <= knockbackAmount; xx += 1)
                    {
                        Tile tile = targetTile.GetTileDown(xx);
                        if (tile != null && tile.isMoveable())
                        {
                            targetUnit.ForceMove(tile);
                        }
                        else
                        {
                            totalDamage += knockbackAmount - (xx - 1);
                            break;
                        }
                    }
                    break;
                }
            }

        }

        if (totalDamage > 0)
            targetUnit.SufferDamage(sourceCard, this, totalDamage);
    }

}
