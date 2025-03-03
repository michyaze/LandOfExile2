using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTrap : Ability
{
    public WeatherTrap trap;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (targetTile.GetTrap() && targetTile.GetTrap().GetComponent<WeatherTrap>().UniqueID == trap.UniqueID)
        {
            var trap = targetTile.GetTrap().GetComponent<WeatherTrap>();
            if (trap.TriggerAbility)
            {
                trap.TriggerAbility.PerformAbility(sourceCard, targetTile, amount);
            }
        }
    }
}
