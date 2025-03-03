using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformOnTile : Ability
{
    public Ability anotherAbility;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        //Perform standard attack
        if (sourceCard is WeatherTrap trap)
        {
            var unit = trap.GetTile().GetUnit();
            if (unit != null)
            {
                anotherAbility.PerformAbility(sourceCard,unit.GetTile());
            }
        }

    }


}
