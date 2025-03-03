using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragonfire : Ability
{

    public int damageAmount = 4;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        List<Unit> mostUnits = new List<Unit>();
        for (int ii = 0; ii < (MenuControl.Instance.battleMenu.boardMenu.totalActiveTiles / 4); ii += 1)
        {

            List<Unit> units = new List<Unit>();
            for (int xx = 0; xx < 4; xx += 1)
            {
                Tile tile = MenuControl.Instance.battleMenu.boardMenu.tiles[ii+(xx * MenuControl.Instance.battleMenu.boardMenu.totalActiveTiles / 4)];
                if (tile.GetUnit() != null && tile.GetUnit().player != sourceCard.player && !units.Contains(tile.GetUnit()))
                {
                    units.Add(tile.GetUnit());
                }
            }

            if (units.Count > mostUnits.Count)
            {
                mostUnits.Clear();
                mostUnits.AddRange(units);
            }
        }
        for (int ii = 0; ii < 4; ii += 1)
        {
            List<Unit> units = new List<Unit>();
            for (int xx = 0; xx < MenuControl.Instance.battleMenu.boardMenu.totalActiveTiles / 4; xx += 1)
            {
                Tile tile = MenuControl.Instance.battleMenu.boardMenu.tiles[(ii*4) + xx];
                if (tile.GetUnit() != null && tile.GetUnit().player != sourceCard.player && !units.Contains(tile.GetUnit()))
                {
                    units.Add(tile.GetUnit());
                }
            }

            if (units.Count > mostUnits.Count)
            {
                mostUnits.Clear();
                mostUnits.AddRange(units);
            }
        }

        if (mostUnits.Count > 0)
        {
            foreach (Unit unit in mostUnits)
            {
                unit.SufferDamage(sourceCard, this, damageAmount);
            }
        }

    }
}
