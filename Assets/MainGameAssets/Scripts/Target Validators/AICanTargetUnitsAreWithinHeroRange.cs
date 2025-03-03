using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICanTargetUnitsAreWithinHeroRange : AICanTarget
{
    public int range = 3;

    public override bool CanTarget(Tile tile)
    {
        foreach (Tile otherTile in GetCard().player.GetHero().GetAdjacentTiles(range))
        {
            if (otherTile.GetUnit() != null && otherTile.GetUnit().player != GetCard().player)
            {
                return true;
            }
        }

        return false;
    }
}
