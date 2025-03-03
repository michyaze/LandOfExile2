using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerOnDamageTargetSameAmount : Trigger
{
    public Ability otherAbilityToPerform;

    public override void UnitDamaged(Card sourceCard, Unit unit, Ability ability, int damageAmount,
        bool triggerUnitDamage = true)
    {

        if (!(GetCard() is NewWeapon) && (!(GetCard() is Unit) || GetCard().GetZone() != MenuControl.Instance.battleMenu.board)) return;
        if (unit == GetCard())
        {
            Tile tile = ((Unit)GetCard()).GetTile();
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), ()=>
            {   
                otherAbilityToPerform.PerformAbility(GetCard(), tile, damageAmount);
            });
        }
    }

}
