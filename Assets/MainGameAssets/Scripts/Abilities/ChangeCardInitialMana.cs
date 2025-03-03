using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCardInitialMana : Ability
{
    public int value;
    public bool useValue = true;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        var finalValue = amount;
        if (useValue)
        {
            finalValue = value;
        }
        sourceCard?.ChangeManaCost(finalValue,this);
    }
}
