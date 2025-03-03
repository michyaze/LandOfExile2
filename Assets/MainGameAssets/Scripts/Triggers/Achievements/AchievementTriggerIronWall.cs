using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementTriggerIronWall : TriggerAchievement
{
    public Effect blockEffectTemplate;
    public int chargesToReach;

    public override void UnitAppliedEffect(Unit unit, Ability ability, Effect effect, int charges)
    {
        if (effect.originalTemplate == blockEffectTemplate)
        {
            if (unit == MenuControl.Instance.battleMenu.player1.GetHero())
            {
                if (effect.remainingCharges >= chargesToReach)
                {
                    MarkAchievementCompleted();
                }
            }
        }
    }
}
