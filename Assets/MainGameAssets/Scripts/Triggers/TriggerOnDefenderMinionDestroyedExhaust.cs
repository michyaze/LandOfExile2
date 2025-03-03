using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOnDefenderMinionDestroyedExhaust : Trigger
{

    public override void UnitDamaged(Card sourceCard, Unit unit, Ability ability, int damageAmount,
        bool triggerUnitDamage = true)
    {
        if (!(GetCard() is NewWeapon) && (!(GetCard() is Unit) || GetCard().GetZone() != MenuControl.Instance.battleMenu.board)) return;

        if (unit is Minion && ability.GetCard() == GetCard() && unit.GetHP() <= 0)
        {
            unit.ExhaustThisCard();
        }

    }

}
