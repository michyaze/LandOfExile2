using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPower : Ability
{


    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        var selfUnit = sourceCard as Unit;
        int maxPower = selfUnit.initialPower;
        //find unit with max power
        foreach (var unit in MenuControl.Instance.battleMenu.GetAllUnitsOnBoard())
        {
            if (unit != selfUnit)
            {
                if (unit.GetPower() > maxPower)
                {
                    maxPower = unit.GetPower();
                }
            }
        }
        
        selfUnit.ChangePower(this,maxPower);
    }
}
