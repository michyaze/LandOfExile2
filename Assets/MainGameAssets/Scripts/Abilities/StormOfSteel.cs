using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormOfSteel : Ability
{
    public int range = 2;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        foreach (Unit unit in sourceCard.player.GetOpponent().cardsOnBoard.ToArray())
        {
            if (sourceCard.player.GetHero().GetAdjacentTiles(range).Contains(unit.GetTile()))
                sourceCard.player.GetHero().ForceAttack(unit.GetTile());
        }
    }
}
