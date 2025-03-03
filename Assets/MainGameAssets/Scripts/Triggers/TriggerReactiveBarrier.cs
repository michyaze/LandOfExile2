using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerReactiveBarrier : Trigger
{
    public bool canTrigger = true;
    public Block blockEffectTemplate;

    public override void UnitDamaged(Card sourceCard, Unit unit, Ability ability, int damageAmount,
        bool triggerUnitDamage = true)
    {
        if (unit == GetCard() && canTrigger)
        {
            canTrigger = false;
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                ((Hero)GetCard()).ApplyEffect(GetCard(), this, blockEffectTemplate, GetEffect().remainingCharges);
            });
        }
    }
}
