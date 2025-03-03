using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CullTheWeak : Ability
{
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        for (int ii = 0; ii < 2; ii += 1)
        {
            Card cardToCull = null;
            foreach (Card card in sourceCard.player.GetOpponent().cardsInDeck)
            {
                if (card is Minion)
                {

                    if (cardToCull == null || ((Minion)card).GetPower() < ((Minion)cardToCull).GetPower())
                    {
                        cardToCull = card;
                    }
                }
            }

            if (cardToCull != null)
            {
                cardToCull.ExhaustThisCard();
            }
        }
    }
}
