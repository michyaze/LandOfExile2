using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullTrap : Ability
{
    public WeatherTrap pulledTrap;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (this.GetTargetValidator() && !CanTargetTile(sourceCard, targetTile))
        {
            return;
        }
List<WeatherTrap> adjacentTraps = new List<WeatherTrap>();
        List<Tile> emptyAdjacentTiles = new List<Tile>();
        foreach (Tile tile in targetTile.GetAdjacentTilesLinear(1))
        {
            if (tile.CanPlaceTrap())
            {
                if (tile.GetTrap() && tile.GetTrap().UniqueID == pulledTrap.UniqueID)
                {
                    //和要拉取的一样就别拉了好吧
                    adjacentTraps.Add(tile.GetTrap());
                    continue;
                }

                emptyAdjacentTiles.Add(tile);
            }
        }

        emptyAdjacentTiles.Shuffle();

        //get all traps
        var allTraps = MenuControl.Instance.battleMenu.boardMenu.traps;
        var toMoveTraps = new List<WeatherTrap>();
        foreach (var trap in allTraps)
        {
            if (trap && trap.UniqueID == pulledTrap.UniqueID)
            {
                if (adjacentTraps.Contains(trap))
                {
                    //和要拉取的一样就别拉了好吧
                    continue;
                }
                toMoveTraps.Add(trap);
            }
        }

        toMoveTraps.Shuffle();

        int moveCount = Math.Min(toMoveTraps.Count,emptyAdjacentTiles.Count);
        for (int i = 0; i < moveCount; i++)
        {
            var trap = toMoveTraps[i];
            var tile = emptyAdjacentTiles[i];
            MenuControl.Instance.battleMenu.boardMenu.MoveTrapToTile(trap, tile);
        }
    }
}