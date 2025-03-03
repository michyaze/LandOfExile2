using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectDamageHitAllUnitsInALineFromHero : Ability
{
    public int damageAmount;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        int finalDamage = damageAmount;
        if (amount > 0) finalDamage = amount;

        Unit thisUnit = sourceCard.player.GetHero();
        int range = MenuControl.Instance.battleMenu.boardMenu.totalActiveTiles / 4;

        List<Unit> unitsHit = new List<Unit>();

        for (int ii = 1; ii <= range; ii += 1)
        {
            if (targetTile == thisUnit.GetTile().GetTileLeft(ii) || (targetTile.GetUnit() is LargeHero largeHero&& largeHero.GetTiles().Contains(thisUnit.GetTile().GetTileLeft(ii))))
            {
                for (int xx = 1; xx <= range; xx += 1)
                {
                    Tile tile = thisUnit.GetTile().GetTileLeft(xx);
                    if (tile != null && tile.GetUnit() != null)
                    {
                        if (!unitsHit.Contains(tile.GetUnit()))
                        {
                            unitsHit.Add(tile.GetUnit());
                            tile.GetUnit().SufferDamage(sourceCard, this, finalDamage);
                        }
                    }
                }
                break;

            }
            else if (targetTile == thisUnit.GetTile().GetTileUp(ii) || (targetTile.GetUnit() is LargeHero && ((LargeHero)targetTile.GetUnit()).GetTiles().Contains(thisUnit.GetTile().GetTileUp(ii))))
            {
                for (int xx = 1; xx <= range; xx += 1)
                {
                    Tile tile = thisUnit.GetTile().GetTileUp(xx);
                    if (tile != null && tile.GetUnit() != null)
                    {
                        if (!unitsHit.Contains(tile.GetUnit()))
                        {
                            unitsHit.Add(tile.GetUnit());
                            tile.GetUnit().SufferDamage(sourceCard, this, finalDamage);
                        }
                    }
                }
                break;
            }
            else if (targetTile == thisUnit.GetTile().GetTileRight(ii) || (targetTile.GetUnit() is LargeHero && ((LargeHero)targetTile.GetUnit()).GetTiles().Contains(thisUnit.GetTile().GetTileRight(ii))))
            {
                for (int xx = 1; xx <= range; xx += 1)
                {
                    Tile tile = thisUnit.GetTile().GetTileRight(xx);
                    if (tile != null && tile.GetUnit() != null)
                    {
                        if (!unitsHit.Contains(tile.GetUnit()))
                        {
                            unitsHit.Add(tile.GetUnit());
                            tile.GetUnit().SufferDamage(sourceCard, this, finalDamage);
                        }
                    }
                }
                break;
            }
            else if (targetTile == thisUnit.GetTile().GetTileDown(ii) || (targetTile.GetUnit() is LargeHero && ((LargeHero)targetTile.GetUnit()).GetTiles().Contains(thisUnit.GetTile().GetTileDown(ii))))
            {
                for (int xx = 1; xx <= range; xx += 1)
                {
                    Tile tile = thisUnit.GetTile().GetTileDown(xx);
                    if (tile != null && tile.GetUnit() != null )
                    {
                        if (!unitsHit.Contains(tile.GetUnit()))
                        {
                            unitsHit.Add(tile.GetUnit());
                            tile.GetUnit().SufferDamage(sourceCard, this, finalDamage);
                        }
                    }
                }
                break;
            }
        }

    }

}



