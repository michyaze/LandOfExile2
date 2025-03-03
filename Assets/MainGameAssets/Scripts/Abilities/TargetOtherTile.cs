using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetOtherTile : Ability
{
    public enum TargetTileType {
        RandomEmptyAdjacentFromThisUnit, AllEmptyAdjacentFromThisUnit, AdjacentTileWithMostUnitsInALineFromMe, RandomEmptyTile, RandomEmptyAdjacentFromMyHero,AllAdjacentFromThisUnitExceptObstacle,AllTiles,AllAdjacentFromThisTileExceptObstacleAndSelf,AllAdjacentFromThisTileExceptObstacle }

    public Ability otherAbilityToPerform;

    public TargetTileType targetTileType;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        var initialUnit = GetCard() as Unit;
        if (initialUnit == null)
        {
            if (targetTile != null)
            {
                
                initialUnit = targetTile.GetUnit();;
            }
        }

        
        if (targetTileType == TargetTileType.RandomEmptyAdjacentFromThisUnit)
        {

            List<Tile> tiles = new List<Tile>();

            if (initialUnit is LargeHero largeHero)
            {
                foreach (Tile tile in largeHero.GetTiles())
                {
                    foreach (Tile tile1 in tile.GetAdjacentTilesLinear())
                    {
                        if (!tiles.Contains(tile1) && tile1.isMoveable())
                        {
                            tiles.Add(tile1);
                        }
                    }
                }
            }
            else
            {
                foreach (Tile tile1 in initialUnit.GetTile().GetAdjacentTilesLinear())
                {
                    if (!tiles.Contains(tile1) && tile1.isMoveable())
                    {
                        tiles.Add(tile1);
                    }
                }
            }


            if (tiles.Count > 0)
            {
                otherAbilityToPerform.PerformAbility(sourceCard, tiles[Random.Range(0, tiles.Count)], amount);
                return;
            }
        }
        else if (targetTileType == TargetTileType.AllEmptyAdjacentFromThisUnit)
        {

            List<Tile> tiles = new List<Tile>();

            if (initialUnit is LargeHero largeHero)
            {
                foreach (Tile tile in largeHero.GetTiles())
                {
                    foreach (Tile tile1 in tile.GetAdjacentTilesLinear())
                    {
                        if (!tiles.Contains(tile1) && tile1.isMoveable())
                        {
                            tiles.Add(tile1);
                        }
                    }
                }
            }
            else
            {
                foreach (Tile tile1 in initialUnit.GetTile().GetAdjacentTilesLinear())
                {
                    if (!tiles.Contains(tile1) && tile1.isMoveable())
                    {
                        tiles.Add(tile1);
                    }
                }
            }


            foreach (Tile tile in tiles)
            {
                otherAbilityToPerform.PerformAbility(sourceCard, tile, amount);

            }
        }
        else if (targetTileType == TargetTileType.AllAdjacentFromThisUnitExceptObstacle)
        {

            List<Tile> tiles = new List<Tile>();

            if (initialUnit is LargeHero)
            {
                foreach (Tile tile in ((LargeHero)initialUnit).GetTiles())
                {
                    foreach (Tile tile1 in tile.GetAdjacentTilesLinear())
                    {
                        if (!tiles.Contains(tile1) && !tile1.GetObstacle())
                        {
                            tiles.Add(tile1);
                        }
                    }
                }
            }
            else
            {
                foreach (Tile tile1 in initialUnit.GetTile().GetAdjacentTilesLinear())
                {
                    if (!tiles.Contains(tile1)  && !tile1.GetObstacle())
                    {
                        tiles.Add(tile1);
                    }
                }
            }


            foreach (Tile tile in tiles)
            {
                otherAbilityToPerform.PerformAbility(sourceCard, tile, amount);

            }
        }else if (targetTileType == TargetTileType.AllAdjacentFromThisTileExceptObstacleAndSelf)
        {

            List<Tile> tiles = new List<Tile>();

            {
                foreach (Tile tile1 in targetTile.GetAdjacentTilesLinear())
                {
                    if (!tiles.Contains(tile1)  && !tile1.GetObstacle())
                    {
                        tiles.Add(tile1);
                    }
                }
            }
            tiles.Add(targetTile);
            foreach (Tile tile in tiles)
            {
                otherAbilityToPerform.PerformAbility(sourceCard, tile, amount);

            }
        }
        else if (targetTileType == TargetTileType.AllAdjacentFromThisTileExceptObstacle)
        {

            List<Tile> tiles = new List<Tile>();

            {
                foreach (Tile tile1 in targetTile.GetAdjacentTilesLinear())
                {
                    if (!tiles.Contains(tile1)  && !tile1.GetObstacle())
                    {
                        tiles.Add(tile1);
                    }
                }
            }
            foreach (Tile tile in tiles)
            {
                otherAbilityToPerform.PerformAbility(sourceCard, tile, amount);

            }
        }


        else if (targetTileType == TargetTileType.AdjacentTileWithMostUnitsInALineFromMe)
        {
            Tile tileToTarget = null;
            int mostUnits = 0;

            int count = 0;
            for (int ii = 0; ii < MenuControl.Instance.battleMenu.boardMenu.totalActiveTiles / 4; ii += 1)
            {
                Tile tile = initialUnit.GetTile().GetTileUp(ii);
                if (tile != null && tile.GetUnit() != null)
                {
                    count += 1;
                }
            }
            if (count > mostUnits)
            {
                mostUnits = count;
                tileToTarget = initialUnit.GetTile().GetTileUp();
            }

            count = 0;
            for (int ii = 0; ii < MenuControl.Instance.battleMenu.boardMenu.totalActiveTiles / 4; ii += 1)
            {
                Tile tile = initialUnit.GetTile().GetTileDown(ii);
                if (tile != null && tile.GetUnit() != null)
                {
                    count += 1;
                }
            }
            if (count > mostUnits)
            {
                mostUnits = count;
                tileToTarget = initialUnit.GetTile().GetTileDown();
            }

            count = 0;
            for (int ii = 0; ii < MenuControl.Instance.battleMenu.boardMenu.totalActiveTiles / 4; ii += 1)
            {
                Tile tile = initialUnit.GetTile().GetTileLeft(ii);
                if (tile != null && tile.GetUnit() != null)
                {
                    count += 1;
                }
            }
            if (count > mostUnits)
            {
                mostUnits = count;
                tileToTarget = initialUnit.GetTile().GetTileLeft();
            }

            count = 0;
            for (int ii = 0; ii < MenuControl.Instance.battleMenu.boardMenu.totalActiveTiles / 4; ii += 1)
            {
                Tile tile = initialUnit.GetTile().GetTileRight(ii);
                if (tile != null && tile.GetUnit() != null)
                {
                    count += 1;
                }
            }
            if (count > mostUnits)
            {
                mostUnits = count;
                tileToTarget = initialUnit.GetTile().GetTileRight();
            }

            if (tileToTarget != null)
            {
                otherAbilityToPerform.PerformAbility(sourceCard, tileToTarget, amount);
            }

        }

        else if (targetTileType == TargetTileType.RandomEmptyTile)
        {
            List<Tile> tiles = new List<Tile>();
            foreach (Tile tile in MenuControl.Instance.battleMenu.boardMenu.tiles)
            {
                if (tile.isMoveable())
                {
                    tiles.Add(tile);
                }
            }

            if (tiles.Count > 0)
            {
                otherAbilityToPerform.PerformAbility(sourceCard, tiles[Random.Range(0, tiles.Count)], amount);
            }

        }
        else if (targetTileType == TargetTileType.RandomEmptyAdjacentFromMyHero)
        {

            List<Tile> tiles = new List<Tile>();
            foreach (Tile tile in sourceCard.player.GetHero().GetAdjacentTiles())
            {
                if (tile.isMoveable())
                    tiles.Add(tile);
            }

            if (tiles.Count > 0)
            {
                otherAbilityToPerform.PerformAbility(sourceCard, tiles[Random.Range(0, tiles.Count)], amount);
                return;
            }
        }else if (targetTileType == TargetTileType.AllTiles)
        {
            List<Tile> tiles = new List<Tile>();
            foreach (Tile tile in MenuControl.Instance.battleMenu.boardMenu.tiles)
            {
                //if (tile.isMoveable())
                {
                    tiles.Add(tile);
                }
            }

            foreach (var tile in tiles)
            {
                otherAbilityToPerform.PerformAbility(sourceCard, tile, amount);
            }

        }


    }
}
