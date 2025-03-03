using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equalize : Ability
{
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        int power = targetTile.GetUnit().GetPower();
        int currentHP = targetTile.GetUnit().GetHP();

        foreach (Tile tile in targetTile.GetAdjacentTilesLinear(1))
        {
            if (tile.GetUnit() is Minion)
            {
                tile.GetUnit().ChangePower(this, power);
                tile.GetUnit().ChangeCurrentHP(this, currentHP);
            }
        }
    }
}
