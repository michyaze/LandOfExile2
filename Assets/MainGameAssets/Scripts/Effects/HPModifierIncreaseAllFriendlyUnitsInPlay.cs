using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPModifierIncreaseAllFriendlyUnitsInPlay : HPModifier
{

    public int increaseAmount = 1;

    public override int ModifyAmount(Unit unit, int currentAmount)
    {
        if (GetCard().player == unit.player && unit.GetZone() == MenuControl.Instance.battleMenu.board && ((Unit)GetCard()).GetZone() == MenuControl.Instance.battleMenu.board)
        {
            return currentAmount + increaseAmount;
        }

        return currentAmount;
    }
}
