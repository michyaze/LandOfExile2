using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AchievementTriggerRealHero : TriggerAchievement
{

    public int summons = 0;

    public override void MinionSummoned(Minion minion)
    {
        if (minion.player == MenuControl.Instance.battleMenu.player1)
        {
            summons += 1;
        }
    }

    public override void GameEnded(bool victory)
    {
        if (victory)
        {
            AdventureItemEncounter encounter = MenuControl.Instance.adventureMenu.GetCurrentAdventureItemEncounter();
            if (encounter != null && encounter.isBoss)
            {

                if (summons == 0)
                    MarkAchievementCompleted();

            }

        }
    }
}
