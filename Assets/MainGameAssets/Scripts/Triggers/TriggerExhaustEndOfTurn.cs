using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerExhaustEndOfTurn : Trigger
{
    public override void TurnEnded(Player player)
    {
        if (GetCard().player == player && GetCard().GetZone() != MenuControl.Instance.battleMenu.removedFromGame)
        {
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                if (GetCard().GetZone() != MenuControl.Instance.battleMenu.removedFromGame)
                {
                    GetCard().ExhaustThisCard();
                }
            });
        }
    }
}
