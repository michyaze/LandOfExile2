using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTakeAim : Trigger
{


    public override void TurnStarted(Player player)
    {
        if (GetCard().player == player)
        {

            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {

                ((Unit)GetCard()).ForceAttack(GetCard().player.GetOpponent().GetHero().GetTile());
                ((Unit)GetCard()).RemoveEffect(GetCard(), this, GetEffect());

            });

        }
    }

    public override void UnitMoved(Unit unit, Tile originalTile, Tile destinationTile)
    {
        if (unit is Hero)
        {
            ((Unit)GetCard()).RemoveEffect(GetCard(), this, GetEffect());
        }
    }
}
