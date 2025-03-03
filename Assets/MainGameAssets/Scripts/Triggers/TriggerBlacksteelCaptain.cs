using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBlacksteelCaptain : Trigger
{
    public Effect copyEffectTemplate;
    public CardTag cardTag;

    public override void TurnStarted(Player player)
    {
        if (player == GetCard().player && GetCard().GetZone() == MenuControl.Instance.battleMenu.board)
        {

            List<Minion> cardsToDraw = new List<Minion>();
            foreach (Card card in player.cardsInDeck)
            {
                if (card.cardTags.Contains(cardTag) && card is Minion)
                {
                    cardsToDraw.Add((Minion)card);
                }
            }
            foreach (Card card in player.cardsInDiscard)
            {
                if (card.cardTags.Contains(cardTag) && card is Minion)
                {
                    cardsToDraw.Add((Minion)card);
                }
            }

            if (cardsToDraw.Count > 0)
            {
                MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                {
                    Minion oldCard = cardsToDraw[Random.Range(0, cardsToDraw.Count)];
                    Minion newCard = (Minion)oldCard.player.CreateCardInGameFromTemplate(oldCard);
                    newCard.DrawThisCard();
                    newCard.ApplyEffect(GetCard(), this, copyEffectTemplate, 0);
                });
            }
        }
    }
}
