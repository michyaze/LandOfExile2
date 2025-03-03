using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementTriggerOgrePate : TriggerAchievement
{
    public int minAmount = 12;

    public override void UnitDamaged(Card sourceCard, Unit unit, Ability ability, int damageAmount,
        bool triggerUnitDamage = true)
    {
        if (unit is LargeHero && ability is Attack)
        {
            if (damageAmount >= minAmount)
                MarkAchievementCompleted();
        }
    }
}
