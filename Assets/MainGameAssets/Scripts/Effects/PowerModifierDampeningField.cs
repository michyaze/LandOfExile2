using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerModifierDampeningField : PowerModifier
{

    public override int ModifyPower(Unit unit, int currentPower, Unit attacker, Unit defender)
    {

        Unit thisUnit = (Unit)GetCard();
        if (thisUnit.GetZone() == MenuControl.Instance.battleMenu.board && unit.GetZone() == MenuControl.Instance.battleMenu.board && unit.GetAdjacentTiles().Contains(thisUnit.GetTile()) && unit.player != thisUnit.player)
        {
            return Mathf.Max(1, currentPower - 2);
        }
        return currentPower;
    }
}
