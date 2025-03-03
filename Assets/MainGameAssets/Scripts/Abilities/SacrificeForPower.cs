using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificeForPower : Ability
{
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (targetTile.GetUnit() && targetTile.GetUnit() is Minion)
        {
            int power = ((Minion)targetTile.GetUnit()).GetPower();
            ((Minion)targetTile.GetUnit()).Sacrifice(sourceCard, this);
            ((Unit)sourceCard).ChangePower(this, ((Unit)sourceCard).currentPower + power);
        }
    }
}
