using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapPowerAndHP : Ability
{
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        Unit unit = targetTile.GetUnit();
        int power = unit.currentPower;
        int hitPoints = unit.currentHP;

        unit.ChangePower(this, hitPoints);
        unit.ChangeCurrentHP(this, power);
    }
}
