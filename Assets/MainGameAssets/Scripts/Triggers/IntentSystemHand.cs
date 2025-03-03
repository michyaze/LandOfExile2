using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntentSystemHand : Trigger
{
    public List<Card> cards = new List<Card>();
    public List<Card> ferocityACards = new List<Card>();

    public virtual List<Card> GetHandCards()
    {
        List<Card> cardsToReturn = new List<Card>();
        cardsToReturn.AddRange(cards);

        if (MenuControl.Instance.eventMenu.isSpecialChallenge && MenuControl.Instance.eventMenu.specialChallengeFerocity)
            cardsToReturn.AddRange(ferocityACards);

        return cardsToReturn;
    }

    public virtual List<Card> GetFollowingHandCards()
    {
        List<Card> cardsToReturn = new List<Card>();
        cardsToReturn.AddRange(cards);

        if (MenuControl.Instance.eventMenu.isSpecialChallenge && MenuControl.Instance.eventMenu.specialChallengeFerocity)
            cardsToReturn.AddRange(ferocityACards);

        return cardsToReturn;
    }
}
