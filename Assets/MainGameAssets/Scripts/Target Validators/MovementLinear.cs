using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementLinear", menuName = "Game Data/Movement Type/Linear", order = 1)]
public class MovementLinear : TargetValidator
{

    public int range = 1;
    public bool canJump;

    public override bool CanUnitTargetTile(Unit unit, Tile tile)
    {
        //Check unit is on board
        if (unit.GetTile() != null)
        {
            //Check tile is unfilled
            if (tile.isMoveable())
            {
                Tile currentTile = unit.GetTile();
                for (int ii = 0; ii < range; ii += 1)
                {
                    currentTile = currentTile.GetTileUp();
                    if (currentTile == tile)
                    {
                        return true;
                    }
                    if (currentTile == null || (!canJump && currentTile.GetUnit() != null)) break;
                }

                currentTile = unit.GetTile();
                for (int ii = 0; ii < range; ii += 1)
                {
                    currentTile = currentTile.GetTileDown();
                    if (currentTile == tile)
                    {
                        return true;
                    }
                    if (currentTile == null || (!canJump && currentTile.GetUnit() != null)) break;
                }
                currentTile = unit.GetTile();
                for (int ii = 0; ii < range; ii += 1)
                {
                    currentTile = currentTile.GetTileLeft();
                    if (currentTile == tile)
                    {
                        return true;
                    }
                    if (currentTile == null || (!canJump && currentTile.GetUnit() != null)) break;
                }
                currentTile = unit.GetTile();
                for (int ii = 0; ii < range; ii += 1)
                {
                    currentTile = currentTile.GetTileRight();
                    if (currentTile == tile)
                    {
                        return true;
                    }
                    if (currentTile == null || (!canJump && currentTile.GetUnit() != null)) break;
                }
            }
        }

        return false;
    }
}
