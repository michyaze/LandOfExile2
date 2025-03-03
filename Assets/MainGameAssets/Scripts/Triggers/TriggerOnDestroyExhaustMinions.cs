using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOnDestroyExhaustMinions : Trigger
{
    public bool enemyMinions;
    public bool myMinions;
    public bool thisMinion;

    public override void MinionDestroyed(Card sourceCard, Ability ability, int damageAmount, Minion minion)
    {
        if ((myMinions && minion.player == GetCard().player) || (enemyMinions && minion.player != GetCard().player) || (thisMinion && minion == GetCard()))
        {
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                if (minion.GetZone() != MenuControl.Instance.battleMenu.board)
                    minion.ExhaustThisCard();
            });
        }
    }
}
