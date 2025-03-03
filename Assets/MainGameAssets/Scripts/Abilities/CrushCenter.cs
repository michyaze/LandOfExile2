using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushCenter : Ability
{
    public int initialDamage = 2;
    public int additionalDamagePerUnit = 2;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        int finalDamage = initialDamage;

        List<Unit> units = new List<Unit>();
        units.Add(targetTile.GetUnit());

        if (targetTile.GetUnit() is LargeHero largeHero)
        {
            foreach (Tile tile in largeHero.GetAdjacentTiles())
            {
                if (tile.GetUnit() != null && !units.Contains(tile.GetUnit()))
                {
                    units.Add(tile.GetUnit());
                    finalDamage += additionalDamagePerUnit;
                }
            }

            //foreach (Tile tile in largeHero.GetDiagonalTiles())
            //{
            //    if (tile.GetUnit() != null && !units.Contains(tile.GetUnit()))
            //    {
            //        units.Add(tile.GetUnit());
            //        finalDamage += additionalDamagePerUnit;
            //    }
            //}

        }
        else
        {
            foreach (Tile tile in targetTile.GetAdjacentTilesLinear())
            {
                if (tile.GetUnit() != null && !units.Contains(tile.GetUnit()))
                {
                    units.Add(tile.GetUnit());
                    finalDamage += additionalDamagePerUnit;
                }
            }

            //foreach (Tile tile in targetTile.GetDiagonalTiles())
            //{
            //    if (tile.GetUnit() != null && !units.Contains(tile.GetUnit()))
            //    {
            //        units.Add(tile.GetUnit());
            //        finalDamage += additionalDamagePerUnit;
            //    }
            //}

        }

        targetTile.GetUnit().SufferDamage(GetCard(), this, finalDamage);
    }
}
