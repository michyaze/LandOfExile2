using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyCanTargetUnitHasNoEffect : AnyCanTarget
{
    public Effect templateEffect;

    public override bool CanTarget(Card sourceCard, Tile tile)
    {
        if (sourceCard != GetCard()) return true;

        return (tile.GetUnit() != null && tile.GetUnit().GetEffectsWithTemplate(templateEffect).Count == 0);
    }
}
