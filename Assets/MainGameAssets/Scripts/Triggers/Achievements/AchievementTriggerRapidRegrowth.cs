using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementTriggerRapidRegrowth : TriggerAchievement
{
    public int minHealing = 20;
    public int currentHealing;

    public override void UnitHealed(Unit unit, Ability ability, int healAmount)
    {
        if (unit.player == MenuControl.Instance.battleMenu.player1)
        {
            currentHealing += healAmount;

            if (currentHealing >= minHealing)
            {
                MarkAchievementCompleted();
            }
        }
    }
}
