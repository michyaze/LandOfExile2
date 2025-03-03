using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDevour : Trigger
    
{

    public override void UnitDamaged(Card sourceCard, Unit unit, Ability ability, int damageAmount,
        bool triggerUnitDamage = true)
    {
        if (!(GetCard() is NewWeapon) && (!(GetCard() is Unit) || GetCard().GetZone() != MenuControl.Instance.battleMenu.board)) return;

        if (ability.GetCard() == GetCard() && unit.GetHP() <= 0)
        {
            int powerToGain = unit.GetPower();
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                if (((Unit)GetCard()).GetZone() == MenuControl.Instance.battleMenu.board)
                    ((Unit)GetCard()).ChangePower(this, ((Unit)GetCard()).currentPower + powerToGain);
            });
        }

    }
}
