using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsModifierAdjacentFriendlyUnits : ActionsModifier
{
    public int amountToAdd;

    public override int ModifyAmount(Unit unit, int currentAmount)
    {

        if (GetCard().GetZone() == MenuControl.Instance.battleMenu.board && unit != GetCard() && unit.player == GetCard().player && ((Unit)GetCard()).GetAdjacentTiles().Contains(unit.GetTile()))
        {
            return currentAmount + amountToAdd;
        }
         
        return currentAmount;
    }
}
