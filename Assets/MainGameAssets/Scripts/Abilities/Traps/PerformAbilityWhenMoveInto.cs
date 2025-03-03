using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformAbilityWhenMoveInto : Ability
{
    public Ability anotherAbility;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        anotherAbility?.PerformAbility(sourceCard,targetTile,amount);
    }
}
