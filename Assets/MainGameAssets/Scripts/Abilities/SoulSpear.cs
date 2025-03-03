using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulSpear : Ability
{
    public int damagePerHit = 2;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        int finalDamage = damagePerHit;

        Unit thisUnit = sourceCard.player.GetHero();
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
                        tile.GetUnit().SufferDamage(sourceCard, this, finalDamage);
                        finalDamage += damagePerHit;
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
                        tile.GetUnit().SufferDamage(sourceCard, this, finalDamage);
                        finalDamage += damagePerHit;
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
                        tile.GetUnit().SufferDamage(sourceCard, this, finalDamage);
                        finalDamage += damagePerHit;
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
                        tile.GetUnit().SufferDamage(sourceCard, this, finalDamage);
                        finalDamage += damagePerHit;
                    }
                }
                break;
            }
        }
    }
}
