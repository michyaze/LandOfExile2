using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerModifierFlanking : PowerModifier
{
    public int amountToAdd;

    public override int ModifyPower(Unit unit, int currentPower, Unit attacker, Unit defender)
    {

        if (defender != null && unit == attacker && unit == GetCard() && unit.GetZone() == MenuControl.Instance.battleMenu.board)
        {
            foreach (Tile tile in defender.GetAdjacentTiles())
            {
                if (tile.GetUnit() != null && tile.GetUnit() != unit && tile.GetUnit().player != defender.player)
                {
                    return currentPower + amountToAdd;
                }

            }
        }

        return currentPower;
    }
}
