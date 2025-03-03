using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AchievementTriggerSeasonalTidings : TriggerAchievement
{


    public override void GameEnded(bool victory)
    {
        if (victory)
        {
            AdventureItemEncounter encounter = MenuControl.Instance.adventureMenu.GetCurrentAdventureItemEncounter();
            if (encounter != null && encounter.isBoss && MenuControl.Instance.areaMenu.areasVisited == 3)
            {

                if (MenuControl.Instance.heroMenu.foundSanta && MenuControl.Instance.heroMenu.foundKrampus)
                {
                    MarkAchievementCompleted();
                }   
            }

        }
    }
}
