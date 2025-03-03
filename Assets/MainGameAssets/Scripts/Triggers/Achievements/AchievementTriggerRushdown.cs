using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AchievementTriggerRushdown : TriggerAchievement
{
    

    public override void UnitAttacks(Unit attacker, Unit defender, Tile targetTile, bool initialAttack)
    {
        if (attacker.player == MenuControl.Instance.battleMenu.player1 && defender.player != attacker.player && defender is Hero && attacker is Minion)
        {
            if (MenuControl.Instance.battleMenu.currentRound == 1)
            {
                MarkAchievementCompleted();
            }
        }
    }
}
