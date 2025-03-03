using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementTriggerFunInteractive : TriggerAchievement
{
    public int enemiesToDestroy = 3;
    public int enemiesDestroyed;

    public override void MinionDestroyed(Card sourceCard, Ability ability, int damageAmount, Minion minion)
    {
        if (minion.player == MenuControl.Instance.battleMenu.playerAI && MenuControl.Instance.battleMenu.currentPlayer == MenuControl.Instance.battleMenu.playerAI)
        {
            enemiesDestroyed += 1;
            if (enemiesDestroyed >= enemiesToDestroy)
            {
                MarkAchievementCompleted();
            }
        }
    }

    public override void TurnEnded(Player player)
    {
        enemiesDestroyed = 0;
    }

}
