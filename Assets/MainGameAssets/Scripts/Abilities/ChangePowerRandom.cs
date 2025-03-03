using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePowerRandom : Ability
{
    public int changePowerMin = 1;
    public int changePowerMax = 4;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        int changePower = Random.Range(changePowerMin, changePowerMax + 1);
        targetTile.GetUnit().ChangePower(this, targetTile.GetUnit().currentPower + changePower);

    }
}
