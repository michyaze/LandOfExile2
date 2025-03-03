using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFutureLoop : Trigger
{
    public override void CardPlayed(Card card, Tile tile, List<Tile> tiles)
    {
        if (card.player == GetCard().player && card is Castable && card.cardTags.Contains(MenuControl.Instance.spellTag))
        {

            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                card.PutIntoZone(MenuControl.Instance.battleMenu.deck);
                card.player.cardsInDeck.Remove(card);
                card.player.cardsInDeck.Insert(0, card);
            });

            GetEffect().ConsumeCharges(this, 1);
        }
    }
}
