using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyCanTargetPlayerHasMana : AnyCanTarget
{
    public int min = -1;
    public int max = -1;

    public override bool CanTarget(Card sourceCard, Tile tile)
    {
        if (sourceCard != GetCard()) return true;

        if (min >= 0 && sourceCard.player.GetCurrentMana() < min) return false;
        if (max >= 0 && sourceCard.player.GetCurrentMana() > max) return false;

        return true;
    }
}
