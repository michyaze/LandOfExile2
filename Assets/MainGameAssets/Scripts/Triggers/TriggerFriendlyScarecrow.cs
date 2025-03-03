using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFriendlyScarecrow : Trigger
{


    public override void UnitDamaged(Card sourceCard, Unit unit, Ability ability, int damageAmount,
        bool triggerUnitDamage = true)
    {
        if (unit == GetCard().player.GetHero() && damageAmount > 0 && GetCard().GetZone() == MenuControl.Instance.battleMenu.board)
        {

            ((Unit)GetCard()).SufferDamage(GetCard(), this, damageAmount);
            GetCard().player.GetHero().Heal(GetCard(), this, damageAmount);
         
        }
    }
}
