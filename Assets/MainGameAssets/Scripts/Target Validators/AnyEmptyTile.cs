using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnyEmptyTile", menuName = "Game Data/Target Type/AnyEmptyTile", order = 1)]
public class AnyEmptyTile : TargetValidator
{

    public override bool CanUnitTargetTile(Unit unit, Tile tile)
    {
        return !tile.GetUnit();
    }
}
