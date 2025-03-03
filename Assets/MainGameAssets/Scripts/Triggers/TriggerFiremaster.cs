using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFiremaster : Trigger
{

    public Effect burnEffectTemplate;

    public override void UnitAppliedEffect(Unit unit, Ability ability, Effect effect, int charges)
    {
        if (unit.player != GetCard().player && effect.originalTemplate == burnEffectTemplate)
        {
            effect.remainingCharges += GetEffect().remainingCharges;
        }
    }
}
