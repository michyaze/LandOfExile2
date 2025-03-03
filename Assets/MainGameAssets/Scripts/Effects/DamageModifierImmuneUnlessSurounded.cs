using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageModifierImmuneUnlessSurounded : DamageModifier
{
    public override int ModifyAmount(Card sourceCard, Ability ability, Unit unit, int currentAmount)
    {
        if (unit == GetCard())
        {
            int emptyAdjacentTiles = 0;
            foreach (Tile tile in unit.GetTile().GetAdjacentTilesLinear(1))
            {
                if (tile != null && tile.isMoveable())
                    emptyAdjacentTiles += 1;
            }

            if (emptyAdjacentTiles > 0)
                return -99999;

        }

        return currentAmount;
    }


}
