using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntentSystemRandomPickedHand : IntentSystemHand
{
    public int numbertoPick;

    List<Card> cardsToPick = new List<Card>();

    public override List<Card> GetHandCards()
    {
        if (!MenuControl.Instance.battleMenu.inBattle) return cards;
        return cardsToPick;
    }

    public override void TurnEnded(Player player)
    {
        if (GetCard().player == player)
        {
            cardsToPick.Clear();
            for (int ii = 0; ii < numbertoPick; ii += 1)
            {
                cardsToPick.Add(cards[Random.Range(0, cards.Count)]);
            }
        }
    }

    public override void HeroSummoned(Hero newHero)
    {
        if (GetCard() == newHero)
        {
            cardsToPick.Clear();
            for (int ii = 0; ii < numbertoPick; ii += 1)
            {
                cardsToPick.Add(cards[Random.Range(0, cards.Count)]);
            }
        }
    }
}

