using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyCanTargetHeroActions : AnyCanTarget
{
    public int min = -1;
    public int max = -1;

    public override bool CanTarget(Card sourceCard, Tile tile)
    {
        if (sourceCard != GetCard()) return true;

        if (min > 0 && GetCard().player.GetHero().remainingActions < min) return false;
        if (max > 0 && GetCard().player.GetHero().remainingActions > max) return false;

        return true;
    }
}
