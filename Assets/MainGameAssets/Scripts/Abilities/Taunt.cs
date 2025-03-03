using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taunt : Ability
{
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        Hero thisHero = sourceCard.player.GetHero();

        foreach (Tile tile in thisHero.GetAdjacentTiles().ToArray())
        {
            if (tile.GetUnit() != null && tile.GetUnit() is Minion)
            {
                tile.GetUnit().ForceAttack(thisHero.GetTile());
            }
        }
    }
}
