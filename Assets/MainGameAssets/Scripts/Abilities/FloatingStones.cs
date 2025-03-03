using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingStones : Ability
{
    public Ability addRange;
    public int powerToAdd = 1;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        List<Unit> units = new List<Unit>();
        units.Add(targetTile.GetUnit());

        foreach (Tile tile in targetTile.GetUnit().GetAdjacentTiles())
        {
            if (tile.GetUnit() != null)
            {
                units.Add(tile.GetUnit());
            }
        }

        foreach (Unit unit in units)
        {
            addRange.PerformAbility(this.GetCard(), unit.GetTile());
            unit.ChangePower(this, unit.currentPower + powerToAdd);

        }

    }
}
