using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveTrap : Ability
{
public bool targetTrap;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        
        if (targetTrap)
        {
            var currenttrap = GetCard() as WeatherTrap;
            if (currenttrap)
            {
                targetTile = currenttrap.GetTile();
            }
        }
        if (targetTile == null)
        {
            return;
        }
        if (targetTile.GetObstacle() != null)
        {
            return;
        }

        var trap = targetTile.GetTrap();
        if (trap == null)
        {
            return;
            
        }
        
        if (trap.Persistent)
        {
            return;
        }
        
        MenuControl.Instance.battleMenu.FullyRemoveTrap(trap);
        
    }
}