using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRestraint : Trigger
{
    public bool canTrigger;

    public override void BeforeEndTurn(Player player)
    {
        if (player == GetCard().player)
        {
            canTrigger = false;
            if (player.GetCurrentMana() > 0)
            {
                canTrigger = true;
            }
        }
    }
    public override void AfterTurnEnded(Player player)
    {
        if (player == GetCard().player && canTrigger)
        {
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                player.ChangeMana(GetEffect().remainingCharges);
            });
        }
    }
}
