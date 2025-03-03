using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSporehuffer : Trigger
{
    public override void TurnStarted(Player player)
    {
        if (player == GetCard().player && GetCard().GetZone() == MenuControl.Instance.battleMenu.board)
        {
            Unit thisUnit = ((Unit)GetCard());

            thisUnit.ChangePower(this, thisUnit.currentPower - 3);

            if (thisUnit.GetPower() < 4)
            {
                thisUnit.SufferDamage(GetCard(), this, 0, true);
            }
        }
    }
}
