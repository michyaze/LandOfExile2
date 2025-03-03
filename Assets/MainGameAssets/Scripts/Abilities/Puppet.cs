using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puppet : Ability
{
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        Minion minion = (Minion)targetTile.GetUnit();

        List<Minion> otherAdjacentMinions = new List<Minion>();
        foreach (Tile tile in minion.GetTile().GetAdjacentTilesLinear())
        {
            if (tile.GetUnit() != null && tile.GetUnit() is Minion)
            {
                otherAdjacentMinions.Add((Minion)tile.GetUnit());
            }
        }

        if (otherAdjacentMinions.Count > 0)
        {
            minion.ForceAttack(otherAdjacentMinions[Random.Range(0, otherAdjacentMinions.Count)].GetTile());
        }
    }
}
