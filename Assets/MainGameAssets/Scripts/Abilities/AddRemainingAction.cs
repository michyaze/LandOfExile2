using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRemainingAction : Ability
{
    public int value = 1;
    public bool useValue = false;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        Unit unit;
        if (targetTile == null)
        {
            unit = sourceCard.player.GetHero();
        }
        else
        {
            unit = targetTile.GetUnit();
        }

        if (unit)
        {
            unit.addRemainingActions(useValue?value:amount);
        }
    }
}
