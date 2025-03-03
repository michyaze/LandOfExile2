using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerStrongFocus : Trigger
{
    public int cardsToDraw;

    public override void TurnEnded(Player player)
    {
        if (!(GetCard() is NewWeapon) && (!(GetCard() is Unit) || GetCard().GetZone() != MenuControl.Instance.battleMenu.board)) return;

        if (GetCard().player == player)
        {
            Hero hero = (Hero)GetCard();
            if (hero.remainingMoves >= hero.GetInitialMoves())
            {
                cardsToDraw = GetEffect().remainingCharges;
                
            }
        }

    }
    public override void AfterTurnEnded(Player player)
    {
        if (player == GetCard().player)
        {
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                for (int ii = 0; ii < cardsToDraw; ii += 1)
                {
                    ((Hero)GetCard()).player.DrawACard();
                }
            });
        }
    }

    public override void GameStarted()
    {
        cardsToDraw = 0;
    }

}
