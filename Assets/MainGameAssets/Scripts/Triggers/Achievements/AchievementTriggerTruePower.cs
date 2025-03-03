using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AchievementTriggerTruePower : TriggerAchievement
{
    public override void UnitDamaged(Card sourceCard, Unit unit, Ability ability, int damageAmount,
        bool triggerUnitDamage = true)
    {
        if (unit == MenuControl.Instance.battleMenu.player1.GetHero() && damageAmount > 0)
        {
            PlayerPrefs.SetInt("TruePower", 0);
        }
    }

    public override void GameEnded(bool victory)
    {
        if (victory)
        {
            AdventureItemEncounter encounter = MenuControl.Instance.adventureMenu.GetCurrentAdventureItemEncounter();
            if (encounter != null && encounter.isBoss && MenuControl.Instance.areaMenu.areasVisited == 2)
            {
                PlayerPrefs.SetInt("TruePower", 1);

            }
            if (encounter != null && encounter.isBoss && MenuControl.Instance.areaMenu.areasVisited == 3)
            {
                if (PlayerPrefs.GetInt("TruePower") == 1)
                {
                    MarkAchievementCompleted();
                }
            }

        }
    }
}
