using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSameMultipleTimes : Ability
{
    public Ability otherAbilityToPerform;
    public int timesToCall = 2;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        for (int ii = 0; ii < timesToCall; ii += 1)
        {
            otherAbilityToPerform.PerformAbility(sourceCard, targetTile, amount);
        }
    }

  
}
