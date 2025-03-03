using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpikedIronPendant : Trigger
{
    public Effect blockEffectTemplate;

    public override void UnitRemovedEffect(Unit unit, Ability ability, Effect effect)
    {
        if (unit.player == GetCard().player)
        {
            if (effect.originalTemplate == blockEffectTemplate)
            {
                MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                {
                    ((Unit)GetCard()).RemoveEffect(GetCard(), this, GetEffect());
                });
            }
        }
    }
}
