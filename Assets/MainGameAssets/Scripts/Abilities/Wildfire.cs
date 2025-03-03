using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wildfire : Ability
{

    public int damageToMinion;
    public int splashDamageToAdjacent;
    public int damageToEnemyHero;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        List<Minion> minions = new List<Minion>();
        foreach (Unit unit in MenuControl.Instance.battleMenu.GetAllUnitsOnBoard())
        {
            if (unit is Minion)
            {
                minions.Add((Minion)unit);
            }
        }

        if (minions.Count > 0)
        {
            Minion minion = minions[Random.Range(0, minions.Count)];
            List<Tile> tiles = minion.GetAdjacentTiles();
            minion.SufferDamage(sourceCard, this, damageToMinion);
            foreach (Tile tile in tiles)
            {
                if (tile.GetUnit() != null)
                {
                    tile.GetUnit().SufferDamage(sourceCard, this, splashDamageToAdjacent);
                }
            }

        }
        else
        {
            sourceCard.player.GetOpponent().GetHero().SufferDamage(sourceCard, this, damageToEnemyHero);
        }
    }
}
