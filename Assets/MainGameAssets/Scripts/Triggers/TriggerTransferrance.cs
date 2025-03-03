using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTransferrance : Trigger
{
    public override void UnitDamaged(Card sourceCard, Unit unit, Ability ability, int damageAmount,
        bool triggerUnitDamage = true)
    {
        if (unit == GetCard().player.GetHero() && damageAmount > 0)
        {
            GetCard().player.GetHero().Heal(GetCard(), this, damageAmount);
            ((Unit)GetCard()).SufferDamage(GetCard(), ability, damageAmount);
            GetEffect().ConsumeCharges(this, 1);
        }
    }
}
