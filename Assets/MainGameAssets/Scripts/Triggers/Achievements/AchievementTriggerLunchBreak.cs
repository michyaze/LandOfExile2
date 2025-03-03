using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AchievementTriggerLunchBreak : TriggerAchievement
{

    public int maxMinutes = 60;

    public override void GameEnded(bool victory)
    {
        if (victory)
        {
            AdventureItemEncounter encounter = MenuControl.Instance.adventureMenu.GetCurrentAdventureItemEncounter();
            if (encounter != null && encounter.isBoss && MenuControl.Instance.areaMenu.areasVisited == 3)
            {

                DateTime currentDate = System.DateTime.Now;
                TimeSpan difference = currentDate.Subtract(MenuControl.Instance.heroMenu.startDate);
                if (difference.TotalSeconds <= maxMinutes * 60)
                {
                    MarkAchievementCompleted();
                }   
            }

        }
    }
}
