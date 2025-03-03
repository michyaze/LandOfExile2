using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emerge : Ability
{
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        Unit unit = targetTile.GetUnit();


        Hero enemyHero = unit.player.GetOpponent().GetHero();

        List<Tile> adjacentTiles = enemyHero.GetTile().GetAdjacentTilesLinear(1);
        if (enemyHero is LargeHero) adjacentTiles = ((LargeHero)enemyHero).GetAdjacentTiles();

        Tile tileToEmergeOn = null;
        if (adjacentTiles.Count > 0)
        {
            foreach (Tile tile in adjacentTiles)
            {
                if (tile.GetUnit() is Minion)
                {
                    tileToEmergeOn = tile;
                    ((Minion)tile.GetUnit()).SufferDamage(sourceCard, this, 0, true);
                    break;
                }
            }
            if (tileToEmergeOn == null)
            {
                tileToEmergeOn = adjacentTiles[Random.Range(0, adjacentTiles.Count)];
            }

            unit.TargetTile(tileToEmergeOn, false);
        }

    }
}
