using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FromTheAether : Ability
{
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        List<Minion> minionTemplates = new List<Minion>();
        foreach (Card card in sourceCard.player.GetOpponent().cardsRemovedFromGame)
        {
            if (card is Minion)
            {
                minionTemplates.Add((Minion)card);
            }
        }

        if (minionTemplates.Count > 0)
        {
            for (int ii = 0; ii < 3; ii += 1)
            {

                List<Tile> emptyTiles = new List<Tile>();



                foreach (Tile tile in targetTile.GetAdjacentTilesLinear(1))
                {
                    if (tile.isMoveable())
                    {
                        emptyTiles.Add(tile);
                    }
                }


                foreach (Minion minion in sourceCard.player.GetMinionsOnBoard())
                {
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
    }
}
