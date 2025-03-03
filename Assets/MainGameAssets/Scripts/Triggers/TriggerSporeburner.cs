using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSporeburner : Trigger
{
    public Effect burnTemplate;

    public override void UnitAttacks(Unit attacker, Unit defender, Tile targetTile, bool initialAttack)
    {
        if (attacker == GetCard())
        {
            defender.ApplyEffect(GetCard(), this, burnTemplate, attacker.GetPower(attacker, defender));
        }
    }
}
