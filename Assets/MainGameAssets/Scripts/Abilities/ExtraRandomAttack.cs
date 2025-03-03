using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraRandomAttack : Ability
{
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (sourceCard.player.GetHero().remainingMoves == sourceCard.player.GetHero().initialMoves)
        {

            foreach (Minion minion in sourceCard.player.GetMinionsOnBoard())
            {
                List<Unit> enemiesAdjacent = new List<Unit>();
                foreach (Tile tile in minion.GetAdjacentTiles())
                {
                    if (tile.GetUnit() != null && tile.GetUnit().player != minion.player)
                    {
                        enemiesAdjacent.Add(tile.GetUnit());
                    }
                }

                if (enemiesAdjacent.Count > 0)
                {
                    Unit enemy = enemiesAdjacent[Random.Range(0, enemiesAdjacent.Count)];
                    minion.ForceAttack(enemy.GetTile());
                }
            }

        }
    }
}
