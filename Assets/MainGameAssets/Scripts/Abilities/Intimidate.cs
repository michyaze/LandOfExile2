using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intimidate : Ability
{

    public int range = 3;
    public Effect effectTemplate;
    public int chargesToApply;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        
        var hero = sourceCard.player.GetHero();

        List<Unit> units = new List<Unit>();

        foreach (Tile tile in hero.GetTiles())
        {
            foreach (Tile tile2 in tile.GetAdjacentTilesLinear(range))
            {
                if (tile2.GetUnit() != null && tile2.GetUnit().player != sourceCard.player)
                {
                    Unit unit = tile2.GetUnit();
                    if (!units.Contains(unit) && unit != hero)
                        units.Add(unit);
                }
            }
        }

        foreach (Unit unit in units)
        {
            unit.ApplyEffect(sourceCard, this, effectTemplate, chargesToApply);
        }
    }
}
