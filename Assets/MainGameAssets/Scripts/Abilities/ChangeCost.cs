using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCost : Ability
{
    public bool PermanentInBattle = false;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        base.PerformAbility(sourceCard, targetTile, amount);
        GetCard().initialCost = Math.Max(0, GetCard().initialCost - 1);
        if (PermanentInBattle)
        {
            GetCard().SetOriginalCost( GetCard().initialCost);
        }
    }
}
