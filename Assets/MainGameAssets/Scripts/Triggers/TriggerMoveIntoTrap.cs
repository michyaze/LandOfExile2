using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerMoveIntoTrap : Trigger
{
    public WeatherTrap triggerTrap;
    public Ability otherAbility;
    public override void UnitMoved(Unit unit, Tile originalTile, Tile destinationTile)
    {
        if (destinationTile == null)
        {
            return;//陷阱移除触发的
        }

        if (unit != GetCard())
        {
            return;
        }
        var trap = destinationTile.GetTrap();
        //if ((GetComponent<WeatherTrap>() == null)) return;
        if (trap != null)
        {
            if (triggerTrap.UniqueID == trap.UniqueID)
            {
                otherAbility.PerformAbility(GetCard(),destinationTile);
            }
        }
    }

}
