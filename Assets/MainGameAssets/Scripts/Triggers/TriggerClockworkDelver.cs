using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerClockworkDelver : Trigger
{
    public List<Effect> effectTemplates = new List<Effect>();
    public List<int> effectCharges = new List<int>();

    public override void UnitDamaged(Card sourceCard, Unit unit, Ability ability, int damageAmount,
        bool triggerUnitDamage = true)
    {
        if (unit == GetCard() && unit.GetHP() > 0 && unit.GetZone() == MenuControl.Instance.battleMenu.board)
        {
            
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                foreach (Effect effect in unit.currentEffects.ToArray())
                {
                    unit.RemoveEffect(GetCard(), this, effect);
                }
                int randomIndex = Random.Range(0, effectTemplates.Count);
                unit.ApplyEffect(GetCard(), this, effectTemplates[randomIndex], effectCharges[randomIndex]);

            });
        }
    }
}
