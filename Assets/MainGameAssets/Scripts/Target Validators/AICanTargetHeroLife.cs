using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICanTargetHeroLife : AICanTarget
{
    public int min = -1;
    public int max = -1;

    public override bool CanTarget(Tile tile)
    {
        if (min > 0 && GetCard().player.GetHero().GetHP() < min) return false;
        if (max > 0 && GetCard().player.GetHero().GetHP() > max) return false;

        return true;
    }
}
