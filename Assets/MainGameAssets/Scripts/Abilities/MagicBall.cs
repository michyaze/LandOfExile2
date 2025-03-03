using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : Ability
{
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        List<Card> spells = new List<Card>();
        foreach (Card card in sourceCard.player.cardsInDeck)
        {
            if (card is Castable & card.cardTags.Contains(MenuControl.Instance.spellTag))
            {
                spells.Add(card);

            }
        }

        if (spells.Count > 0)
            spells[Random.Range(0, spells.Count)].DrawThisCard();

        spells.Clear();
        foreach (Card card in sourceCard.player.cardsInDiscard)
        {
            if (card is Castable & card.cardTags.Contains(MenuControl.Instance.spellTag))
            {
                spells.Add(card);

            }
        }

        if (spells.Count > 0)
            spells[Random.Range(0, spells.Count)].DrawThisCard();

    }
}
