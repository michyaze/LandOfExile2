using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManaWarp : Trigger
{
    public override void CardPlayed(Card card, Tile tile, List<Tile> tiles)
    {
        if (card.player == GetCard().player && card is Castable && !(card is Artifact) && card.cardTags.Contains(MenuControl.Instance.spellTag))
        {
            Card cardToReturn = card;
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                Card newCard = card.player.CreateCardInGameFromTemplate(card.cardTemplate);
                newCard.DrawThisCard();
                newCard.gameObject.AddComponent<TriggerCopyExhausts>();
                GetEffect().ConsumeCharges(this, 1);
            });
        }
    }
}
