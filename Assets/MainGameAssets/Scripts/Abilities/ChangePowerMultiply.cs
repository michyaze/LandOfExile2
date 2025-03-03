using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePowerMultiply : Ability
{
    public int amountToMultiply = 2;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        int newBase = (targetTile.GetUnit().GetPower() * (amountToMultiply - 1)) + targetTile.GetUnit().currentPower;

        targetTile.GetUnit().ChangePower(this, newBase);
    }
}
