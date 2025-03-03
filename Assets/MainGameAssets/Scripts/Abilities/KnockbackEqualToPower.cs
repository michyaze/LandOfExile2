using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackEqualToPower : Ability
{

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        int knockbackAmount = ((Unit)GetCard()).GetPower();

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
                if (targetTile == thisUnit.GetTile().GetTileLeft(ii))
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
                else if (targetTile == thisUnit.GetTile().GetTileUp(ii))
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
                else if (targetTile == thisUnit.GetTile().GetTileRight(ii))
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
                else if (targetTile == thisUnit.GetTile().GetTileDown(ii))
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
