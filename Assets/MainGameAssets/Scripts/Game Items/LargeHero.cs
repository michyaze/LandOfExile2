using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeHero : Hero
{
    public override List<Tile> GetTiles()
    {
        if (GetZone() == MenuControl.Instance.battleMenu.board)
        {
            return MenuControl.Instance.battleMenu.boardMenu.GetTilesOfLargeHero(this);
        }

        return null;
    }

    public override List<Tile> GetAdjacentTiles(int range = 1)
    {
        List<Tile> adjacentTiles = new List<Tile>();
        if (GetZone() == MenuControl.Instance.battleMenu.board)
        {

            foreach (Tile tile in GetTiles())
            {
                foreach (Tile tile2 in tile.GetAdjacentTilesLinear(range))
                {
                    if (!GetTiles().Contains(tile2))
                    {
                        adjacentTiles.Add(tile2);
                    }
                }
            }
        }
    

        return adjacentTiles;
    }
    
    public override List<Tile> GetTilesRight(int spaces = 1)
    {
        var res = new List<Tile>();
        foreach (var tile in GetTiles())
        {
            res.Add(tile.GetTileRight(spaces));
        }

        foreach (var tile in GetTiles())
        {
            res.Remove(tile);
        }

        return res;
    }
    public override List<Tile> GetTilesUp(int spaces = 1)
    {var res = new List<Tile>();
        foreach (var tile in GetTiles())
        {
            res.Add(tile.GetTileUp(spaces));
        }

        foreach (var tile in GetTiles())
        {
            res.Remove(tile);
        }

        return res;
    }
    public override List<Tile> GetTilesDown(int spaces = 1)
    {
        var res = new List<Tile>();
        foreach (var tile in GetTiles())
        {
            res.Add(tile.GetTileDown(spaces));
        }

        foreach (var tile in GetTiles())
        {
            res.Remove(tile);
        }

        return res;
    }
    public override List<Tile> GetTilesLeft(int spaces = 1)
    {
        var res = new List<Tile>();
        foreach (var tile in GetTiles())
        {
            res.Add(tile.GetTileLeft(spaces));
        }

        foreach (var tile in GetTiles())
        {
            res.Remove(tile);
        }

        return res;
    }

    public override List<Tile> GetDiagonalTiles()
    {
        List<Tile> diagonalTiles = new List<Tile>();
        if (GetZone() == MenuControl.Instance.battleMenu.board)
        {

            foreach (Tile tile in GetTiles())
            {
                foreach (Tile tile2 in tile.GetDiagonalTiles())
                {
                    if (!GetTiles().Contains(tile2))
                    {
                        diagonalTiles.Add(tile2);
                    }
                }
            }
        }

        foreach (Tile tile in GetAdjacentTiles())
        {
            if (diagonalTiles.Contains(tile))
            {
                diagonalTiles.Remove(tile);
            }
        }


        return diagonalTiles;
    }
}
