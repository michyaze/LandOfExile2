using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageModifierOldFortification : DamageModifier
{

    public override int ModifyAmount(Card sourceCard, Ability ability, Unit unit, int currentAmount)
    {
        if (((Unit)GetCard()).GetZone() == MenuControl.Instance.battleMenu.board && unit != GetCard())
        {
            if (((Unit)GetCard()).GetTile().GetAdjacentTilesLinear(1).Contains(unit.GetTile()))
            {
                return Mathf.FloorToInt(currentAmount / 2f);
            }
        }

        return currentAmount;
    }


}
