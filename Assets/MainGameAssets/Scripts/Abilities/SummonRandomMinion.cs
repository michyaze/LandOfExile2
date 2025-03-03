using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonRandomMinion : Ability
{
    public List<Minion> minionTemplates = new List<Minion>();

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        List<Tile> emptyTiles = new List<Tile>();

        if (targetTile.GetUnit() is LargeHero)
        {
            foreach (Tile tile1 in ((LargeHero)targetTile.GetUnit()).GetTiles())
            {
                foreach (Tile tile in tile1.GetAdjacentTilesLinear(1))
                {
                    if (tile.isMoveable())
                    {
                        emptyTiles.Add(tile);
                    }
                }
            }
        }
        else
        {

            foreach (Tile tile in targetTile.GetAdjacentTilesLinear(1))
            {
                if (tile.isMoveable())
                {
                    emptyTiles.Add(tile);
                }
            }
        }

        foreach (Minion minion in sourceCard.player.GetMinionsOnBoard()) {
            foreach (Tile tile in minion.GetTile().GetAdjacentTilesLinear(1))
            {
                if (tile.isMoveable())
                {
                    emptyTiles.Add(tile);
                }
            }
        }


        if (emptyTiles.Count > 0)
        {
            Card templateCard = minionTemplates[Random.Range(0, minionTemplates.Count)];

            Card newCard = sourceCard.player.CreateCardInGameFromTemplate(templateCard);
            newCard.TargetTile(emptyTiles[Random.Range(0, emptyTiles.Count)], false);

        }
    }
}
