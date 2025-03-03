using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThousandBlades : Ability
{
    public int totalDamage = 10;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        Hero hero = sourceCard.player.GetHero();
        List<Unit> enemies = new List<Unit>();
        foreach (Tile tile in hero.GetTile().GetAdjacentTilesLinear(2))
        {
            if (tile.GetUnit() != null && tile.GetUnit().player != hero.player)
            {
                enemies.Add(tile.GetUnit());
            }
        }

        if (enemies.Count > 0)
        {
            int damage = Mathf.FloorToInt(totalDamage / (float)enemies.Count);
            foreach (Unit unit in enemies)
            {
                unit.SufferDamage(sourceCard, this, damage);
            }
        }
    }
}
