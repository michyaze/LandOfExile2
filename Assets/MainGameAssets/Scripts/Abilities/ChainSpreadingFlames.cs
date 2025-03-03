using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainSpreadingFlames : Chain
{
    public Effect burnEffectTemplate;

    public override bool CanHitUnit(Unit unit)
    {
        if (!base.CanHitUnit(unit)) return false;

        if (unit.GetEffectsWithTemplate(burnEffectTemplate).Count == 0) return false;

        return true;
    }

}
