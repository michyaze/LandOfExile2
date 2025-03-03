using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementLinearLargeHero", menuName = "Game Data/Movement Type/LinearLargeHero", order = 1)]
public class MovementLinearLargeHero : TargetValidator
{
    public int range = 1;
    public bool canJump;

    bool LargeHeroCanFitOnTile(Tile tile, LargeHero largeHero)
    {
        //Assume tile is top left corner
        if (tile.GetUnit() != null && tile.GetUnit() != largeHero) return false;
        if (tile.GetTileRight() == null) return false;
        if (tile.GetTileRight().GetUnit() != null && tile.GetTileRight().GetUnit() != largeHero) return false;
        if (tile.GetTileDown() == null) return false;
        if (tile.GetTileDown().GetUnit() != null && tile.GetTileDown().GetUnit() != largeHero) return false;
        if (tile.GetTileDown().GetTileRight() == null) return false;
        if (tile.GetTileDown().GetTileRight().GetUnit() != null &&
            tile.GetTileDown().GetTileRight().GetUnit() != largeHero) return false;

        return true;
    }

    public override bool CanUnitTargetTile(Unit unit, Tile tile)
    {
        //Check unit is on board
        if (unit.GetTile() != null)
        {
            if (unit is LargeHero largeHero)
            {
                //Check tile is unfilled
                if (LargeHeroCanFitOnTile(tile, largeHero))
                {
                    Tile currentTile = unit.GetTile();
                    for (int ii = 0; ii < range; ii += 1)
                    {
                        currentTile = currentTile.GetTileUp();
                        if (currentTile == tile)
                        {
                            return true;
                        }

                        if (currentTile == null || (!canJump && !LargeHeroCanFitOnTile(currentTile, largeHero))) break;
                    }

                    currentTile = unit.GetTile();
                    for (int ii = 0; ii < range; ii += 1)
                    {
                        currentTile = currentTile.GetTileDown();
                        if (currentTile == tile)
                        {
                            return true;
                        }

                        if (currentTile == null || (!canJump && !LargeHeroCanFitOnTile(currentTile, largeHero))) break;
                    }

                    currentTile = unit.GetTile();
                    for (int ii = 0; ii < range; ii += 1)
                    {
                        currentTile = currentTile.GetTileLeft();
                        if (currentTile == tile)
                        {
                            return true;
                        }

                        if (currentTile == null || (!canJump && !LargeHeroCanFitOnTile(currentTile, largeHero))) break;
                    }

                    currentTile = unit.GetTile();
                    for (int ii = 0; ii < range; ii += 1)
                    {
                        currentTile = currentTile.GetTileRight();
                        if (currentTile == tile)
                        {
                            return true;
                        }

                        if (currentTile == null || (!canJump && !LargeHeroCanFitOnTile(currentTile, largeHero))) break;
                    }
                }
            }
        }

        return false;
    }
}