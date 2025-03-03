using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerThrow : Ability
{
    public int targetsToHit = 2;
    public int damageAmount = 1;
    public int range = 2;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        //Deal 1 damage to two different enemy targets within range 2. 

        List<Unit> unitsInRange = new List<Unit>();
        foreach (Tile tile in sourceCard.player.GetHero().GetAdjacentTiles(range))
        {
            if (tile.GetUnit() != null && tile.GetUnit().player != sourceCard.player && !unitsInRange.Contains(tile.GetUnit()))
            {
                unitsInRange.Add(tile.GetUnit());
            }
        }

        for (int ii =0; ii < targetsToHit; ii += 1)
        {
            if (unitsInRange.Count > 0)
            {
                Unit unit = unitsInRange[Random.Range(0, unitsInRange.Count)];
                unit.SufferDamage(sourceCard, this, damageAmount);
                unitsInRange.Remove(unit);
            }
        }

    }
}
