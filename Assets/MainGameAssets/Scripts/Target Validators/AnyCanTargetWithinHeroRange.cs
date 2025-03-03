using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyCanTargetWithinHeroRange : AnyCanTarget
{
    public int range = 2;

    public override bool CanTarget(Card sourceCard, Tile tile)
    {
        if (sourceCard != GetCard()) return true;

        return (sourceCard.player.GetHero().GetAdjacentTiles(range).Contains(tile));
    }
}
