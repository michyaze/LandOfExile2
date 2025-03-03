using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeInsultedYourMother : Ability
{
    public int numberOfAttacks = 1;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        Unit targetUnit = targetTile.GetUnit();
        List<Unit> surroundingUnits = new List<Unit>();
        foreach (Tile tile in targetTile.GetUnit().GetAdjacentTiles())
        {
            if (tile.GetUnit() != null && tile.GetUnit() != targetTile.GetUnit() && !surroundingUnits.Contains(tile.GetUnit()))
            {
                surroundingUnits.Add(tile.GetUnit());
            }
        }

        foreach (Unit unit in surroundingUnits)
        {
            for (int ii = 0; ii < numberOfAttacks; ii += 1)
            {
                MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {

                if (targetUnit.GetZone() == MenuControl.Instance.battleMenu.board && unit.GetZone() == MenuControl.Instance.battleMenu.board)
                {
                    unit.ForceAttack(targetUnit.GetTile());
                }

            });
            }

        }
    }
}
