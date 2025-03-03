using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyCanTargetMinionInRangeOfLargeHero : AnyCanTarget
{
    public int range = 3;

    public override bool CanTarget(Card sourceCard, Tile tile)
    {
        if (sourceCard != GetCard()) return true;

        foreach (Tile tile2 in (sourceCard.player.GetHero()).GetTiles())
        {
            if (tile2.GetAdjacentTilesLinear(range).Contains(tile))
            {
                return true;
            }
        }

        return false;
    }
}
