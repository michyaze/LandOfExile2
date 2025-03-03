using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Raze : Ability
{
    public int numberOfCards;
    public Ability dependantAbilityToPerform;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        for (int ii = 0; ii < numberOfCards; ii += 1)
        {
            if (sourceCard.player.cardsInDeck.Count > 0)
            {
                sourceCard.player.cardsInDeck[0].RazeThisCard();
            }
            else if (sourceCard.player.cardsInDiscard.Count > 0)
            {
                sourceCard.player.ShuffleDiscardIntoDeck();
                sourceCard.player.cardsInDeck[0].RazeThisCard();
            } else
            {
                return;
            }

        }

        if (dependantAbilityToPerform != null)
        {
            dependantAbilityToPerform.PerformAbility(sourceCard, targetTile, amount);
        }
    }
}
