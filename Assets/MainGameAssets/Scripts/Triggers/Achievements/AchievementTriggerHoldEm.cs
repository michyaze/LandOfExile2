using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementTriggerHoldEm : TriggerAchievement
{
    public override void CardDrawn(Card card)
    {
        if (card.player == MenuControl.Instance.battleMenu.player1)
        {
            if (card.player.cardsInDeck.Count > 0) return;
            if (card.player.cardsInDiscard.Count > 0) return;
            if (card.player.cardsOnBoard.Count > 1) return;
            if (card.player.cardsRemovedFromGame.Count > 0) return;

            MarkAchievementCompleted();
        }
    }
}
