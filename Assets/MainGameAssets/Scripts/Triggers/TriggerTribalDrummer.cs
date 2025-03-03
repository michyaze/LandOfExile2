using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTribalDrummer : Trigger
{
    public override void TurnStarted(Player player)
    {
        if (player == GetCard().player && GetCard().GetZone()== MenuControl.Instance.battleMenu.board && ((Unit)GetCard()).GetPower() >= 5)
        {
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
              {
                  GetCard().player.GetHero().ChangePower(this, GetCard().player.GetHero().currentPower + 1);
              });
        }
    }
}
