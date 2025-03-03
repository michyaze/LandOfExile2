using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AchievementTriggerMagicTrick : TriggerAchievement
{

    public int minShuffles = 3;
    public int shuffles;

    public override void TurnStarted(Player player)
    {
        shuffles = 0;
    }

    public override void PlayerShuffledDeck(Player player)
    {
        if (player == MenuControl.Instance.battleMenu.player1)
        {
            shuffles += 1;
            if (shuffles >= minShuffles) MarkAchievementCompleted();
        }
    }
}
