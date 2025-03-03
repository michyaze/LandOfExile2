using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDeck : Ability
{
    public List<Card> cardsInDeck = new List<Card>();

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        Player player = sourceCard.player;

        foreach (Card card in player.cardsInDeck.ToArray())
        {
            card.RemoveFromGame();
        }
        foreach (Card card in player.cardsInDiscard.ToArray())
        {
            card.RemoveFromGame();
        }
        int handCardsCount = player.cardsInHand.Count;
        foreach (Card card in player.cardsInHand.ToArray())
        {
            card.RemoveFromGame();
        }

        foreach (Card cardTemplate in cardsInDeck)
        {
            Card newCard = player.CreateCardInGameFromTemplate(cardTemplate);
            newCard.PutIntoZone(MenuControl.Instance.battleMenu.discard);
        }
        player.ShuffleDeck();

        for (int ii = 0; ii < handCardsCount; ii += 1)
        {
            player.DrawACard();
        }
    }
}
