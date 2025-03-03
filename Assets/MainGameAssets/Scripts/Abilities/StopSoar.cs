using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSoar : Ability
{
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        sourceCard.StopFly();
    }
}
