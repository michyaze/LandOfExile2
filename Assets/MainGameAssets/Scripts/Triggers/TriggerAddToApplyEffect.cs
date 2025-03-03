using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAddToApplyEffect : Trigger
{

    public Effect effectTemplate;

    public override void UnitAppliedEffect(Unit unit, Ability ability, Effect effect, int charges)
    {
        if (unit.player != GetCard().player && effect.originalTemplate == effectTemplate)
        {
            effect.remainingCharges += GetEffect().remainingCharges;
        }
    }
}
