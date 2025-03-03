using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetOtherUnitsOnTrap : Ability
{
    public OpposingTargetType opposingTargetType;
    public List<WeatherTrap> traps;
    public Ability anotherAbility;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        foreach (var unit in MenuControl.Instance.battleMenu.GetAllUnitsOnBoard())
        {
            if (CardUtils.isOpposingTargetType(unit, this, opposingTargetType))
            {
                foreach (var trap in traps)
                {
                    if (unit.GetTile().GetTrap().UniqueID == trap.UniqueID)
                    {
                        anotherAbility.PerformAbility(sourceCard, unit.GetTile(), amount);
                        break;
                    }
                }
            }
        }
    }
}