using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSoulAbsorption : Trigger
{
    public bool canTrigger = true;
    public override void MinionDestroyed(Card sourceCard, Ability ability, int damageAmount, Minion minion)
    {
        if (GetCard().player == MenuControl.Instance.battleMenu.currentPlayer)
        {
            if (minion.player == GetCard().player)
            {
                if (canTrigger)
                {
                    canTrigger = false;
                    MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                    {
                        GetCard().player.ChangeMana(GetEffect().remainingCharges);
                    });
                }
            }
        }
    }

    public override void TurnEnded(Player player)
    {
        canTrigger = true;
    }
}
