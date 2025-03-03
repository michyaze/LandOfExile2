using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagesRing : Ability
{
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        foreach (Card card in GetCard().player.cardsInDeck)
        {
            if (card.GetCost() > 0)
            {
                card.DrawThisCard();
                return;
            }
        }
    }
}
