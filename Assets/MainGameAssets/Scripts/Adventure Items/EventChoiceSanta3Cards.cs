using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceSanta3Cards : EventChoice
{
    public override void PerformChoice()
    {
        List<Card> eligibleCards = MenuControl.Instance.heroMenu.heroPath.pathCards;
        eligibleCards.AddRange(MenuControl.Instance.heroMenu.GetUnlockedMinionCards());

        List<Card> niceCards = new List<Card>();
        foreach (Card card in eligibleCards)
        {
            if (card.level == 1 && card is Minion)
            {
                niceCards.Add(card);
            }
        }

        for (int ii = 0; ii < 3; ii++){
            Card randomCard = niceCards[Random.Range(0,niceCards.Count)];
            Card newCard = MenuControl.Instance.heroMenu.AddCardToDeck(randomCard);
            newCard.cardTags.Add(MenuControl.Instance.niceTag);
        }
        MenuControl.Instance.dataControl.SaveData();
        CloseEvent();
    }
}

