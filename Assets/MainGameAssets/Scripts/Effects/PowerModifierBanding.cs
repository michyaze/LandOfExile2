using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerModifierBanding : PowerModifier
{
    public float multiplier = 2f;
    public int additionalAmount = 0;

    public override int ModifyPower(Unit unit, int currentPower, Unit attacker, Unit defender)
    {

        if (defender != null && unit == attacker && unit == GetCard() && unit.GetZone() == MenuControl.Instance.battleMenu.board)
        {
            int otherEnemiesAdjacent = 0;
            foreach (Tile tile in defender.GetTile().GetAdjacentTilesLinear(1))
            {
                if (tile.GetUnit() != null && tile.GetUnit() != attacker && tile.GetUnit().player != defender.player)
                {
                    otherEnemiesAdjacent += 1;
                }
            }

            if (otherEnemiesAdjacent > 0)
            {

                int newPower = Mathf.FloorToInt(currentPower * multiplier);

                return newPower + additionalAmount;
            }
        }

        return currentPower;
    }
}
