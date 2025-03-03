using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MultiTargetAbility
{

    public int damageAmount = 5;

    public override bool CanTargetTiles(Card card, List<Tile> tiles)
    {
        if (tiles.Count == 2)
        {
            return tiles[0] != tiles[1];
        }

        return false;
    }

    public override void PerformAbility(Card sourceCard, List<Tile> targetTiles, int amount = 0)
    {
        Unit myUnit = targetTiles[0].GetUnit();

        Unit targetUnit = targetTiles[1].GetUnit();
        if (targetUnit != null)
        {
            targetUnit.SufferDamage(sourceCard, this, damageAmount);
            myUnit.SufferDamage(sourceCard, this, damageAmount);
        }
        else
        {
            myUnit.ForceMove(targetTiles[1]);
        }

    }
}
