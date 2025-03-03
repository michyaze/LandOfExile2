using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBurningSoulArrows : Trigger
{

    public int damagePerCharge = 4;
    public override void TurnStarted(Player player)
    {
        if (player == GetCard().player)
        {
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(),() =>
            {
                int totalCharges = GetEffect().remainingCharges;
                for (int ii = 0; ii < totalCharges; ii += 1)
                {
                    GetCard().player.GetOpponent().GetHero().SufferDamage(GetCard(), this, damagePerCharge);
                }
                
                ((Unit)GetCard()).RemoveEffect(GetCard(), this, GetEffect());
            });
        }
    }
}
