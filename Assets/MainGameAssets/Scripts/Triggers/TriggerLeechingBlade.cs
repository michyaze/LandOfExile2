using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLeechingBlade : Trigger
{
    public override void UnitDamaged(Card sourceCard, Unit unit, Ability ability, int damageAmount,
        bool triggerUnitDamage = true)
    {
        if (ability == GetCard().activatedAbility)
        {
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {

                ((Unit)GetCard()).Heal(GetCard(), this, damageAmount);

            });
        }
    }
}
