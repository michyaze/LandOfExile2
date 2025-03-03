using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsingMass : Trigger
{
    public Effect weakenEffectTemplate;

    public override void UnitDamaged(Card sourceCard, Unit unit, Ability ability, int damageAmount,
        bool triggerUnitDamage = true)
    {
        if (unit == GetCard() && ability.GetCard() is Minion)
        {
            Minion minion = (Minion)ability.GetCard();

            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                if (minion.GetZone() == MenuControl.Instance.battleMenu.board)
                {
                    minion.ChangePower(this, minion.currentPower - 1);
                    unit.ChangePower(this, unit.currentPower + 1);
                }
            });
        }
    }

}
