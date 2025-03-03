using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrampusRod : Ability
{
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        foreach (Card card in GetCard().player.cardsInHand)
        {
            if (card.cardTags.Contains(MenuControl.Instance.naughtyTag))
            {
                card.initialCost = 0;
            }
        }
    }
}
