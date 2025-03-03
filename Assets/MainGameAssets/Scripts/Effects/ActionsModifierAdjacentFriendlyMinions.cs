using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsModifierAdjacentFriendlyMinions : ActionsModifier
{
    public int amountToAdd;

    public override int ModifyAmount(Unit unit, int currentAmount)
    {

        if (GetCard().GetZone() == MenuControl.Instance.battleMenu.board && unit != GetCard() && unit is Minion && unit.player == GetCard().player && ((Unit)GetCard()).GetAdjacentTiles().Contains(unit.GetTile()))
        {
            return currentAmount + amountToAdd;
        }
         
        return currentAmount;
    }
}
