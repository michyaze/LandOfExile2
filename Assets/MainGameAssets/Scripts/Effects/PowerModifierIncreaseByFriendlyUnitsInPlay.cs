using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerModifierIncreaseByFriendlyUnitsInPlay : PowerModifier
{
    public override int ModifyPower(Unit unit, int currentPower, Unit attacker, Unit defender)
    {
        if (GetCard() == unit && unit.GetZone() == MenuControl.Instance.battleMenu.board)
        {
            return currentPower -1 + GetCard().player.cardsOnBoard.Count;
        }

        return currentPower;
    }
}
