using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFlameblessedBlades : Trigger
{
    public Effect burnEffectTemplate;

    public override void UnitAttacks(Unit attacker, Unit defender, Tile targetTile, bool initialAttack)
    {
        if (attacker.player == GetCard().player && attacker.GetAdjacentTiles().Contains(((Unit)GetCard()).GetTile()))
        {
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () => {

                if (defender.GetZone() == MenuControl.Instance.battleMenu.board)
                {
                    defender.ApplyEffect(GetCard(), this, burnEffectTemplate, GetEffect().remainingCharges);
                }
            
            });
        }
    }
}
