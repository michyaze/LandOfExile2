using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapPositionsOfFriendlyUnits : Ability
{
    public bool includeHero = false;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        List<Unit> units = new List<Unit>();
        foreach (Card card in sourceCard.player.cardsOnBoard)
        {
            if (!(card is LargeHero))
            {
                units.Add((Unit)card);
            }
        }
        if (!includeHero) units.Remove(sourceCard.player.GetHero());

        List<Tile> tiles = new List<Tile>();
        foreach (Unit unit in units.ToArray())
        {

                tiles.Add(unit.GetTile());
            
        }
        tiles.Shuffle();

        for (int ii = 0; ii < units.Count; ii += 1)
        {
            units[ii].ForceMove(tiles[ii]);
        }
    }
}
