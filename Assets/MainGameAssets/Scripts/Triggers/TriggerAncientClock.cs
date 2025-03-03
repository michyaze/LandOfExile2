using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAncientClock : Trigger
{

    public override void TurnStarted(Player player)
    {
        if (player == GetCard().player && MenuControl.Instance.battleMenu.currentRound % 2 == 0 && MenuControl.Instance.battleMenu.currentRound >= 2)
        {
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                player.DrawACard();
            });
        }
    }

}
