using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AchievementTriggerLotOfBoars : TriggerAchievement
{

    public int minLevel = 15;

    public override void GameStarted()
    {
        if (MenuControl.Instance.heroMenu.currentLevel >= minLevel)
        {
            MarkAchievementCompleted();
        }
    }
}
