using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AchievementTriggerDetermination : TriggerAchievement
{

    public override void GameEnded(bool victory)
    {
        if (victory)
        {
            AdventureItemEncounter encounter = MenuControl.Instance.adventureMenu.GetCurrentAdventureItemEncounter();
            if (encounter != null)
            {
                if (MenuControl.Instance.battleMenu.player1.cardsOnBoard.Count > 1) return;
                if (MenuControl.Instance.battleMenu.player1.GetHero().currentHP > 1) return;

                MarkAchievementCompleted();

            }

        }
    }
}
