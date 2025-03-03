using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNightmorph : Trigger
{
    public override void UnitAppliedEffect(Unit unit, Ability ability, Effect effect, int charges)
    {
        if (unit == GetCard().player.GetOpponent().GetHero())
        {
            ((Hero)GetCard()).ApplyEffect(GetCard(), this, effect.originalTemplate, charges);
        }
    }
}
