using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBombInfusion : Trigger
{
    public int selfDamage = 3;
    public int adjacentDamage = 3;

    public override void UnitMoved(Unit unit, Tile originalTile, Tile destinationTile)
    {
        if (unit == GetCard())
        {
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                Tile newTile = unit.GetTile();
                if (newTile)
                {
                    unit.SufferDamage(GetCard(), this, selfDamage);
                    foreach (Tile tile in newTile.GetAdjacentTilesLinear())
                    {
                        if (tile.GetUnit() != null)
                        {
                            tile.GetUnit().SufferDamage(GetCard(), this, adjacentDamage);
                        }
                    }
                }

            });
        }
    }
}
