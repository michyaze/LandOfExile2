using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sacrifice : Ability
{
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (targetTile.GetUnit() && targetTile.GetUnit() is Minion)
            ((Minion)targetTile.GetUnit()).Sacrifice(sourceCard, this);
    }
}
