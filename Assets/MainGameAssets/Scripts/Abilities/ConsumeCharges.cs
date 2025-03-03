using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeCharges : Ability
{
    public int chargesToConsume = 1;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        GetEffect().ConsumeCharges(this, chargesToConsume);
    }

}
