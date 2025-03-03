using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerModifierIncreaseAdjacentFriendlyUnits : PowerModifier
{

    public int powerAlteredAmount = 1;
    public int range = 1;
    public bool includeSelf;
    public bool addCharges;

    public override int ModifyPower(Unit unit, int currentPower, Unit attacker, Unit defender)
    {
        if (GetCard().player == unit.player)
        {
            Unit thisUnit = (Unit)GetCard();
            if (thisUnit.GetZone() == MenuControl.Instance.battleMenu.board && unit.GetZone() == MenuControl.Instance.battleMenu.board && unit.GetTile().GetAdjacentTilesLinear(range).Contains(thisUnit.GetTile()))
            {
                return currentPower + powerAlteredAmount + (addCharges ? remainingCharges : 0);
            }
            if (includeSelf && unit == thisUnit)
            {
                return currentPower + powerAlteredAmount + (addCharges ? remainingCharges : 0);
            }
        }

        return currentPower;
    }
}
