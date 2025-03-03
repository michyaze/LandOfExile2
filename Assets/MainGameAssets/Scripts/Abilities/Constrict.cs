using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constrict : Ability
{
    public Card neckSectionTemplate;

    public int damageAmount = 2;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        foreach (Unit unit in sourceCard.player.GetOpponent().cardsOnBoard)
        {
            foreach (Tile tile in unit.GetAdjacentTiles())
            {
                if (tile.GetUnit() != null && tile.GetUnit().cardTemplate.UniqueID == neckSectionTemplate.UniqueID)
                {
                    unit.SufferDamage(sourceCard, this, damageAmount);
                    break;
                }
            }

        }
    }
}
