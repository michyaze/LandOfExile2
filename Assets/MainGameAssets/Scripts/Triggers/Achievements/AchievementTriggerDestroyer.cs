using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementTriggerDestroyer : TriggerAchievement
{
    public int minMinionsToDestroy = 5;
    public int minionsDestroyed;

    public override void TurnStarted(Player player)
    {
        minionsDestroyed = 0;
    }


    public override void MinionDestroyed(Card sourceCard, Ability ability, int damageAmount, Minion minion)
    {
        if (minion.player == MenuControl.Instance.battleMenu.playerAI)
        {
            minionsDestroyed += 1;
            if (minionsDestroyed >= minMinionsToDestroy)
            {
                MarkAchievementCompleted();
            }
        }
    }
}
