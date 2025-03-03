using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Erupt : Ability
{
    public int eruptions = 6;
    public int damageAmount = 1;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        List<Tile> tiles = new List<Tile>();
        tiles.AddRange(MenuControl.Instance.battleMenu.boardMenu.tiles);

        for (int ii = 0; ii<eruptions; ii += 1)
        {
            Tile tile = tiles[Random.Range(0, tiles.Count)];
            tiles.Remove(tile);

            if (tile.GetUnit() != null)
            {
                tile.GetUnit().SufferDamage(sourceCard, this, amount == 0 ? damageAmount : amount);
            }
        }
    }
}
