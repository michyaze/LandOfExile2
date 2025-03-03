using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectDamageHitHighestNumberOfUnitsInALine : Ability
{
    public int damageAmount;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        List<Unit> unitsInLine = new List<Unit>();

        //Try rows first
        for (int ii = 0; ii < 4; ii += 1)
        {
            List<Unit> units = new List<Unit>();

            Tile firstTile = MenuControl.Instance.battleMenu.boardMenu.tiles[ii * (MenuControl.Instance.battleMenu.boardMenu.totalActiveTiles / 4)];

            if (firstTile.GetUnit() != null)
            {
                units.Add(firstTile.GetUnit());
            }

            for (int xx =0; xx < MenuControl.Instance.battleMenu.boardMenu.totalActiveTiles / 4; xx += 1)
            {
                Tile otherTile = firstTile.GetTileRight(xx);
                if (otherTile != null && otherTile.GetUnit() != null)
                {
                    units.Add(otherTile.GetUnit());
                }
            }

            if (unitsInLine.Count < units.Count)
            {
                unitsInLine.Clear();
                unitsInLine.AddRange(units);
            }
        }

        //Now columns
        for (int ii = 0; ii < MenuControl.Instance.battleMenu.boardMenu.totalActiveTiles / 4; ii += 1)
        {
            List<Unit> units = new List<Unit>();

            Tile firstTile = MenuControl.Instance.battleMenu.boardMenu.tiles[ii];

            if (firstTile.GetUnit() != null)
            {
                units.Add(firstTile.GetUnit());
            }

            for (int xx = 0; xx < 4; xx += 1)
            {
                Tile otherTile = firstTile.GetTileDown(xx);
                if (otherTile != null && otherTile.GetUnit() != null)
                {
                    units.Add(otherTile.GetUnit());
                }
            }

            if (unitsInLine.Count < units.Count)
            {
                unitsInLine.Clear();
                unitsInLine.AddRange(units);
            }
        }

        foreach (Unit unit in unitsInLine)
        {
            unit.SufferDamage(sourceCard, this, damageAmount);
        }
    }

}



