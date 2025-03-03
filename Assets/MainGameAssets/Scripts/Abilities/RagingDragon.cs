using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RagingDragon : Ability
{
    public int damageAmount = 8;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        List<Unit> enemyUnits = new List<Unit>();
        foreach (Card card in sourceCard.player.GetOpponent().cardsOnBoard) {
            enemyUnits.Add((Unit)card);
        }

        enemyUnits.OrderBy(x => x.GetHP()).First().SufferDamage(sourceCard, this, damageAmount);

    }
}
