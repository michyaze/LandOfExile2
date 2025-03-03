using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFlare : Ability
{
    //瞄准一个空格子或单位。与其相邻的所有单位（大型英雄无效）都会被击退一格，如果它们与另一单位或战场边缘碰撞，则会受到3点伤害；与之碰撞的单位也会受到3点伤害
    public int knockbackAmount = 1;
    public int damageAmount = 3;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        Tile originalTile = targetTile;

        foreach (Tile adjacentTile in originalTile.GetAdjacentTilesLinear(1))
        {

            Unit targetUnit = adjacentTile.GetUnit();
            Unit otherUnit = null;
            if (!(targetUnit is LargeHero) && targetUnit != null)
            {

                int totalDamage = 0;

                for (int ii = 1; ii <= 4; ii += 1)
                {
                    if (adjacentTile == originalTile.GetTileLeft(ii))
                    {
                        for (int xx = 1; xx <= knockbackAmount; xx += 1)
                        {
                            Tile tile = adjacentTile.GetTileLeft(xx);
                            if (tile != null && tile.isMoveable())
                            {
                                targetUnit.MoveToTile(tile);
                            }
                            else
                            {
                                totalDamage += knockbackAmount - (xx - 1);
                                if (tile != null)
                                    otherUnit = tile.GetUnit();
                                break;
                            }
                        }
                        break;

                    }
                    else if (adjacentTile == originalTile.GetTileUp(ii))
                    {
                        for (int xx = 1; xx <= knockbackAmount; xx += 1)
                        {
                            Tile tile = adjacentTile.GetTileUp(xx);
                            if (tile != null && tile.isMoveable())
                            {
                                targetUnit.MoveToTile(tile);
                            }
                            else
                            {
                                totalDamage += knockbackAmount - (xx - 1);
                                if (tile != null)
                                    otherUnit = tile.GetUnit();
                                break;
                            }
                        }
                        break;
                    }
                    else if (adjacentTile == originalTile.GetTileRight(ii))
                    {
                        for (int xx = 1; xx <= knockbackAmount; xx += 1)
                        {
                            Tile tile = adjacentTile.GetTileRight(xx);
                            if (tile != null && tile.isMoveable())
                            {
                                targetUnit.MoveToTile(tile);
                            }
                            else
                            {
                                totalDamage += knockbackAmount - (xx - 1);
                                if (tile != null)
                                    otherUnit = tile.GetUnit();
                                break;
                            }
                        }
                        break;
                    }
                    else if (adjacentTile == originalTile.GetTileDown(ii))
                    {
                        for (int xx = 1; xx <= knockbackAmount; xx += 1)
                        {
                            Tile tile = adjacentTile.GetTileDown(xx);
                            if (tile != null && tile.isMoveable())
                            {
                                targetUnit.MoveToTile(tile);
                            }
                            else
                            {
                                totalDamage += knockbackAmount - (xx - 1);
                                if (tile != null)
                                    otherUnit = tile.GetUnit();
                                break;
                            }
                        }
                        break;
                    }
                }



                if (totalDamage > 0)
                {
                    targetUnit.SufferDamage(GetCard(), this, damageAmount);
                    if (otherUnit != null)
                        otherUnit.SufferDamage(GetCard(), this, damageAmount);
                }

            }
        }

    }
}

