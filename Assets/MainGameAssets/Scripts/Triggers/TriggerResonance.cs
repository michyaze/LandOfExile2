using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerResonance : Trigger
{

    public Effect blockTemplate;
    public int amountToGain = 1;
    public int damageThreshold = 10;

    public override void UnitDamaged(Card sourceCard, Unit unit, Ability ability, int damageAmount,
        bool triggerUnitDamage = true)
    {
        if (!(GetCard() is NewWeapon) && (!(GetCard() is Unit) || GetCard().GetZone() != MenuControl.Instance.battleMenu.board)) return;

        if (damageAmount > 0 && ability.GetCard() is Castable && ability.GetCard().cardTags.Contains(MenuControl.Instance.spellTag) && ability.GetCard().player == GetCard().player)
        {
            GetEffect().remainingCharges += damageAmount;
            if (GetEffect().remainingCharges >= damageThreshold)
            {
                int blockInstancesToGain = Mathf.FloorToInt(GetEffect().remainingCharges / (float)damageThreshold);
                GetEffect().remainingCharges -= blockInstancesToGain * damageThreshold;

                MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                {
                    ((Unit)GetCard()).ApplyEffect(GetCard(), this, blockTemplate, blockInstancesToGain * amountToGain);
                });

            }
        }

    }

}
