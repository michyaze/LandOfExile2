using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetOtherUnitsInLineFromHero : Ability
{
    public Ability otherAbility;

    public int range = 99;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        Unit thisUnit = sourceCard.player.GetHero();

        List<Tile> tiles = new List<Tile>();
        if ((thisUnit is LargeHero))
        {
            tiles.AddRange(((LargeHero)thisUnit).GetTiles());
        }
        else
        {
            tiles.Add(thisUnit.GetTile());
        }

        foreach (Tile tile2 in tiles) {
            for (int ii = 1; ii <= range; ii += 1)
            {
                if (targetTile == tile2.GetTileLeft(ii))
                {
                    for (int xx = 1; xx <= range; xx += 1)
                    {
                        Tile tile = tile2.GetTileLeft(xx);
                        if (tile != null && tile.GetUnit() != null && tile.GetUnit() != thisUnit)
                        {
                            otherAbility.PerformAbility(sourceCard, tile, amount);
                        }
                    }
                    break;

                }
                else if (targetTile == tile2.GetTileUp(ii))
                {
                    for (int xx = 1; xx <= range; xx += 1)
                    {
                        Tile tile = tile2.GetTileUp(xx);
                        if (tile != null && tile.GetUnit() != null && tile.GetUnit() != thisUnit)
                        {
                            otherAbility.PerformAbility(sourceCard, tile, amount);
                        }
                    }
                    break;
                }
                else if (targetTile == tile2.GetTileRight(ii))
                {
                    for (int xx = 1; xx <= range; xx += 1)
                    {
                        Tile tile = tile2.GetTileRight(xx);
                        if (tile != null && tile.GetUnit() != null && tile.GetUnit() != thisUnit)
                        {
                            otherAbility.PerformAbility(sourceCard, tile, amount);
                        }
                    }
                    break;
                }
                else if (targetTile == tile2.GetTileDown(ii))
                {
                    for (int xx = 1; xx <= range; xx += 1)
                    {
                        Tile tile = tile2.GetTileDown(xx);
                        if (tile != null && tile.GetUnit() != null && tile.GetUnit() != thisUnit)
                        {
                            otherAbility.PerformAbility(sourceCard, tile, amount);
                        }
                    }
                    break;
                }
            }
        }

    }

}
