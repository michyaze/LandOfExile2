using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementTriggerTitanicStrength : TriggerAchievement
{
    public int minPower = 12;

    public override void UnitChangedPower(Unit unit, Ability ability, int oldValue)
    {
        if (unit == MenuControl.Instance.battleMenu.player1.GetHero())
        {
            if (unit.GetPower() >= minPower)
            {
                MarkAchievementCompleted();
            }
        }
    }

    public override void UnitAppliedEffect(Unit unit, Ability ability, Effect effect, int charges)
    {
        if (unit == MenuControl.Instance.battleMenu.player1.GetHero())
        {
            if (unit.GetPower() >= minPower)
            {
                MarkAchievementCompleted();
            }
        }
    }
}
