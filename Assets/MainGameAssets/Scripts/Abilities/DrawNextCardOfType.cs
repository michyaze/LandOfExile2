using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawNextCardOfType : Ability
{
    public bool playerDraws = true;
    public bool enemyDraws;
    public int cardsToDraw = 1;
    public CardTag cardTag;
    public bool minions;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        for (int ii = 0; ii < cardsToDraw; ii += 1)
        {

            if (playerDraws)
            {
                bool drawn = false;
                foreach (Card card in GetCard().player.cardsInDeck)
                {
                    if ((cardTag != null && card.cardTags.Contains(cardTag))
                        && (!minions || card is Minion))
                    {
                        drawn = true;
                        card.DrawThisCard();
                        break;
                    }
                }

                if (!drawn)
                {
                    foreach (Card card in GetCard().player.cardsInDiscard)
                    {
                        if ((cardTag != null && card.cardTags.Contains(cardTag))
                         && (!minions || card is Minion))
                        {
                            drawn = true;
                            card.DrawThisCard();
                            break;
                        }
                    }
                }
            }

            if (enemyDraws)
            {
                bool drawn = false;
                foreach (Card card in GetCard().player.GetOpponent().cardsInDeck)
                {
                    if ((cardTag != null && card.cardTags.Contains(cardTag))
                        && (!minions || card is Minion))
                    {
                        drawn = true;
                        card.DrawThisCard();
                        break;
                    }
                }

                if (!drawn)
                {
                    foreach (Card card in GetCard().player.GetOpponent().cardsInDiscard)
                    {
                        if ((cardTag != null && card.cardTags.Contains(cardTag))
                         && (!minions || card is Minion))
                        {
                            drawn = true;
                            card.DrawThisCard();
                            break;
                        }
                    }
                }
            }
        }
    }
}
