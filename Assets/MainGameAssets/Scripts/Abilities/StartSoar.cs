using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSoar : Ability
{
    public override bool CanTargetTile(Card card, Tile tile)
    {
        return true;
    }
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        var unit = targetTile.GetUnit();
        if (unit)
        {
            
            unit.StartFly();
        }
    }
}
