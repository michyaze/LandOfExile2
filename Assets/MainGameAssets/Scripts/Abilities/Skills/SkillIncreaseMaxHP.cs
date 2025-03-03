using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillIncreaseMaxHP : Ability
{
    public int amountToIncrease = 1;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (sourceCard != null && sourceCard is Unit unit)
        {
            unit.initialHP += amountToIncrease;
            unit.currentHP += amountToIncrease;
        }
        else
        {
            
            MenuControl.Instance.heroMenu.hero.initialHP += amountToIncrease;
            MenuControl.Instance.heroMenu.hero.currentHP += amountToIncrease;
        }
    }
}
