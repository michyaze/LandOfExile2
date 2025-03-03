using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetherSwap : MultiTargetAbility
{

    public override bool CanTargetTiles(Card card, List<Tile> tiles)
    {
        if (tiles.Count == 2)
        {
            return tiles[0] != tiles[1] && tiles[1].GetUnit() != null && !(tiles[1].GetUnit() is LargeHero) && tiles[1].GetUnit().player == GetCard().player;
        }

        return false;
    }

    public override void PerformAbility(Card sourceCard, List<Tile> targetTiles, int amount = 0)
    {
        Unit unit1 = targetTiles[0].GetUnit();

        Unit unit2 = targetTiles[1].GetUnit();

        if (unit1 == null || unit1 is LargeHero) return;

        Tile tile1 = unit1.GetTile();
        if (tile1 == null) return;

        if (unit2 == null || unit2 is LargeHero) return;

        Tile tile2 = targetTiles[1];
        if (tile2 == null) return;

        unit1.ChangePosition(unit2);

    }
}
