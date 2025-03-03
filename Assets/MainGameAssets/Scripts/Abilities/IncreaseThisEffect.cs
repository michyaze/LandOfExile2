using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseThisEffect : Ability
{
    public int overrideAmount = 1;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        ((Unit)GetCard()).ApplyEffect(sourceCard, this, GetEffect().originalTemplate,overrideAmount);
    }

}