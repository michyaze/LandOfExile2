using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerModifierCowardlyThug : PowerModifier
{

    public int modifierAmount;

    public override int ModifyPower(Unit unit, int currentPower, Unit attacker, Unit defender)
    {
        if (unit == GetCard())
        {
            foreach (Tile tile in unit.GetAdjacentTiles())
            {
                if (tile.GetUnit() != null && tile.GetUnit().IsPinned() && tile.GetUnit().player != unit.player)
                {
                    return currentPower + modifierAmount;
                }
            }
        }

        return currentPower;
    }
}
