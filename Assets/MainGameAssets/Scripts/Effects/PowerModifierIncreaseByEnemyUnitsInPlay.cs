using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerModifierIncreaseByEnemyUnitsInPlay : PowerModifier
{
    public int amounterPerUnit = 1;

    public override int ModifyPower(Unit unit, int currentPower, Unit attacker, Unit defender)
    {
        if (GetCard() == unit && unit.GetZone() == MenuControl.Instance.battleMenu.board)
        {
            return currentPower + (amounterPerUnit * GetCard().player.GetOpponent().cardsOnBoard.Count);
        }

        return currentPower;
    }
}
