using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDirtyFighting : Trigger
{
    public Effect stunEffectTemplate;
    public bool canTrigger = true;

    public override void UnitAttacks(Unit attacker, Unit defender, Tile targetTile, bool initialAttack)
    {
        if (attacker is Minion && attacker.player != GetCard().player && defender == GetCard().player.GetHero())
        {
            if (canTrigger)
            {
                canTrigger = false;
                MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                {
                    attacker.ApplyEffect(GetCard(), this, stunEffectTemplate, 2);
                });
            }

        }
    }

    public override void TurnEnded(Player player)
    {
        canTrigger = true;
    }
}
