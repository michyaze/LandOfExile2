using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerKeepAway : Trigger
{

    public Knockback knockbackAbility;

    public override void UnitAttacks(Unit attacker, Unit defender, Tile targetTile, bool initialAttack)
    {
        if (attacker == GetCard() && initialAttack)
        {
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                if (defender.GetZone() == MenuControl.Instance.battleMenu.board)
                {
                    if (GetEffect())
                    {
                        
                        knockbackAbility.knockbackAmount = GetEffect().remainingCharges;
                    }
                    knockbackAbility.PerformAbility(GetCard(), defender.GetTile());
                }
            });
        }
    }
}
