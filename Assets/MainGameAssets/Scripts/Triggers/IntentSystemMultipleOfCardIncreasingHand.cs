using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntentSystemMultipleOfCardIncreasingHand : IntentSystemHand
{
    public int additionalAmount;
    List<Card> cardsToPick = new List<Card>();
    List<Card> cardsToPick2 = new List<Card>();

    public override List<Card> GetHandCards()
    {
        return cardsToPick;
    }

    public override List<Card> GetFollowingHandCards()
    {
        return cardsToPick2;
    }

    public override void TurnEnded(Player player)
    {
        if (GetCard().player == player)
        {
            cardsToPick.Clear();
            for (int ii = 0; ii < Mathf.FloorToInt(MenuControl.Instance.battleMenu.currentRound/ ((Hero)GetCard()).GetIntentSystem().hands.Count) + additionalAmount; ii += 1)
            {
                cardsToPick.Add(cards[Random.Range(0, cards.Count)]);
            }

            cardsToPick2.Clear();
            for (int ii = 0; ii < (MenuControl.Instance.battleMenu.currentRound / ((Hero)GetCard()).GetIntentSystem().hands.Count) + additionalAmount + 1; ii += 1)
            {
                cardsToPick2.Add(cards[Random.Range(0, cards.Count)]);
            }
        }
    }

    public override void HeroSummoned(Hero newHero)
    {
        if (GetCard() == newHero)
        {
            cardsToPick.Clear();
            for (int ii = 0; ii < Mathf.FloorToInt(MenuControl.Instance.battleMenu.currentRound / ((Hero)GetCard()).GetIntentSystem().hands.Count) + additionalAmount; ii += 1)
            {
                cardsToPick.Add(cards[Random.Range(0, cards.Count)]);
            }

            cardsToPick2.Clear();
            for (int ii = 0; ii < Mathf.FloorToInt(MenuControl.Instance.battleMenu.currentRound / ((Hero)GetCard()).GetIntentSystem().hands.Count) + additionalAmount + 1; ii += 1)
            {
                cardsToPick2.Add(cards[Random.Range(0, cards.Count)]);
            }
        }
    }

}

