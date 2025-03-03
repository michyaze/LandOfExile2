using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillIncreaseActions : Ability
{
    public int amountToIncrease = 1;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (sourceCard != null && sourceCard is Unit unit)
        {
            unit.initialActions += amountToIncrease;
        }
        else
        {
            MenuControl.Instance.heroMenu.hero.initialActions += amountToIncrease;
        }
    }
}
