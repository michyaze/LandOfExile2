using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementTriggerPopOff : TriggerAchievement
{
    public int minCardsToplay = 10;
    public int cardsPlayed;

    public override void TurnStarted(Player player)
    {
        cardsPlayed = 0;
    }

    public override void CardPlayed(Card card, Tile tile, List<Tile> tiles)
    {
        if (card.player == MenuControl.Instance.battleMenu.player1)
        {
            cardsPlayed += 1;
            if (cardsPlayed >= minCardsToplay)
            {
                MarkAchievementCompleted();
            }
        }
    }
}
