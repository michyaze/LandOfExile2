using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgeSpirit : Ability
{
    public Card primordialFlameTemplate;


    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        List<Minion> flameMinions = new List<Minion>();
        foreach (Card card in MenuControl.Instance.battleMenu.GetAllUnitsOnBoard())
        {
            if (card.cardTemplate.UniqueID == primordialFlameTemplate.UniqueID)
            {
                flameMinions.Add((Minion)card);
            }
        }
        flameMinions.Shuffle();

        foreach (Minion minion in flameMinions)
        {
            if (minion.GetZone() == MenuControl.Instance.battleMenu.board)
            {
                List<Tile> tiles = new List<Tile>();
                foreach (Tile tile in MenuControl.Instance.battleMenu.boardMenu.tiles)
                {
                    if (tile.isMoveable())
                    {
                        tiles.Add(tile);
                    }
                }

                if (tiles.Count > 0)
                {
                    tiles.Shuffle();
                    minion.TargetTile(tiles[0], false);
                }

            }
        }
    }
}
