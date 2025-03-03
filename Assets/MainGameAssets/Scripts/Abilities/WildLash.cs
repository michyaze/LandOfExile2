using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildLash : Ability
{
    public int initialDamage = 6;
    public int reductionForEachAdjacent = 2;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        int finalDamage = initialDamage;

        List<Unit> units = new List<Unit>();
        units.Add(targetTile.GetUnit());

        foreach (Tile tile in targetTile.GetUnit().GetAdjacentTiles())
        {
            if (tile.GetUnit() != null && !units.Contains(tile.GetUnit()))
            {
                units.Add(tile.GetUnit());
            }
        }

        finalDamage = Mathf.Max(1, finalDamage - (reductionForEachAdjacent * (units.Count - 1)));

        targetTile.GetUnit().SufferDamage(GetCard(), this, finalDamage);
    }
}
