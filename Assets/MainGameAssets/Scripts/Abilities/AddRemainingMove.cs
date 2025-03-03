using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRemainingMove : Ability
{
    public int value = 1;
    public bool useValue = false;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (targetTile == null)
        {
            //this happens because apply effect when hero is not put on board
            return;
        }
        if (useValue)
        {
            amount = value;
        }
        var unit = targetTile.GetUnit();
        if (unit)
        {
            unit.addRemainingMoves(amount);
        }
    }
}
