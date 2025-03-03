using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerModifierDistanceToTarget : PowerModifier
{
    public int additionPerDistance = 1;

    public override int ModifyPower(Unit unit, int currentPower, Unit attacker, Unit defender)
    {

        if (defender != null && unit == attacker && unit == GetCard() && unit.GetZone() == MenuControl.Instance.battleMenu.board)
        {
            int finalPower = currentPower;
            if (unit.GetTile().GetAdjacentTilesLinear(1).Contains(defender.GetTile()))
            {
                
            }
            else if (unit.GetTile().GetAdjacentTilesLinear(2).Contains(defender.GetTile()))
            {
                finalPower += additionPerDistance * 1;
            }
            else if (unit.GetTile().GetAdjacentTilesLinear(3).Contains(defender.GetTile()))
            {
                finalPower += additionPerDistance * 2;
            }
            else if (unit.GetTile().GetAdjacentTilesLinear(4).Contains(defender.GetTile()))
            {
                finalPower += additionPerDistance * 3;
            }

            return finalPower;

        }

        return currentPower;
    }
}
