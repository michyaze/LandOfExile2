using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reforge : Ability
{
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        Minion minion = (Minion)targetTile.GetUnit();

        minion.SufferDamage(sourceCard,this,0,true);

        minion.TargetTile(targetTile, false);
        minion.ChangePower(this, minion.initialPower * 2);
        minion.ChangeCurrentHP(this, minion.GetInitialHP() * 2);
    }
}
