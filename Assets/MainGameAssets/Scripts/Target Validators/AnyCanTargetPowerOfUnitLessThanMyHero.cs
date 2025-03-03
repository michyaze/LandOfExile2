using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyCanTargetPowerOfUnitLessThanMyHero : AnyCanTarget
{
    public override bool CanTarget(Card sourceCard, Tile tile)
    {
        if (sourceCard != GetCard()) return true;

        return (tile.GetUnit() != null && GetCard().player.GetHero().GetPower() > tile.GetUnit().GetPower());
    }
}
