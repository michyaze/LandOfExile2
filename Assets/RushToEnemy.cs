using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushToEnemy : Ability
{
    int distanceBetweenTiles(Tile tile1, Tile tile2)
    {
        return Mathf.Abs(tile1.GetRow() - tile2.GetRow()) + Mathf.Abs(tile1.GetCol() - tile2.GetCol());
    }
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        // bfs 搜索所有target周围可达的格子，找到离敌方英雄最近，且移动最少的格子，最多搜索到remaining move的格子距离
        var unit = targetTile.GetUnit();
        if (unit)
        {
            var enemyHero = unit.player.GetOpponent().GetHero();
            int remainingMove = unit.remainingMoves;
            if (enemyHero)
            {
                var enemyTile = enemyHero.GetTile();
                Queue<(Tile tile,int move)> queue = new Queue<(Tile,int)>();
                queue.Enqueue((targetTile,0));
                HashSet<Tile> visited = new HashSet<Tile>();
                visited.Add(targetTile);

                int closestDistanceToEnemy = distanceBetweenTiles(targetTile, enemyTile);
                Tile closestTile = targetTile;
                int move = 0;
                while (queue.Count > 0)
                {
                    var queueIntem = queue.Dequeue();
                    var tile = queueIntem.tile;
                    move = queueIntem.move;
                    
                    if (distanceBetweenTiles(tile, enemyTile) < closestDistanceToEnemy)
                    {
                        closestDistanceToEnemy = distanceBetweenTiles(tile, enemyTile);
                        closestTile = tile;
                    }

                    if (move == remainingMove)
                    {
                        break;
                    }
                    move++;
                    foreach (var neighbor in tile.GetAdjacentTilesLinear())
                    {
                        if (neighbor == enemyTile)
                        {
                            unit.useRemainingMoves(move-1);
                            Move(unit, tile, targetTile);
                            return;
                        }
                        if (neighbor.isMoveable()  && !visited.Contains(neighbor))
                        {
                            queue.Enqueue((neighbor,move));
                            visited.Add(neighbor);
                        }
                    }
                }
                unit.useRemainingMoves(move);
                Move(unit, closestTile, targetTile);

            }
        }
        
    }

    void Move(Unit unit,Tile tile,Tile targetTile)
    {
        
        //这个tile和敌方英雄相邻，移动到这个tile
        if (tile != targetTile)
        {
            unit.MoveToTile(tile);
        }
    }
}
