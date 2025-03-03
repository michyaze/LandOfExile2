using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Divide : Ability
{
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        Card templateMinion = targetTile.GetUnit().cardTemplate;

        targetTile.GetUnit().SufferDamage(sourceCard, this, 0, true);

        List<Tile> emptyTiles = new List<Tile>();
        foreach (Tile tile in targetTile.GetAdjacentTilesLinear(1))
        {
            if (tile.isMoveable())
            {
                emptyTiles.Add(tile);
            }
        }

        if (emptyTiles.Count > 0)
        {
            emptyTiles.Shuffle();

            for (int ii = 0; ii < 2; ii += 1)
            {
                Card newCard = sourceCard.player.CreateCardInGameFromTemplate(templateMinion);
                newCard.TargetTile(emptyTiles[ii], false);
            }

        }
    }
}
