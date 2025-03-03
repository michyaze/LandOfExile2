using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SummoningAdjacentOwnUnits", menuName = "Game Data/Summoning Type/Adjacent", order = 1)]
public class SummoningAdjacentOwnUnits : TargetValidator
{

    public override bool CanUnitTargetTile(Unit unit, Tile tile)
    {
        //Check tile is unfilled
        if (tile.isMoveable())
        {

            foreach (Tile adjacentTile in tile.GetAdjacentTilesLinear())
            {
                if (adjacentTile.GetUnit() != null  && adjacentTile.GetUnit().player == unit.player)
                    return true;
            }
        }

        return false;
    }
}
