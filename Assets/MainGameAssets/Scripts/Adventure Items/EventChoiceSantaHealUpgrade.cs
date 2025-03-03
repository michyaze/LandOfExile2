using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceSantaHealUpgrade : EventChoice
{
    public override void PerformChoice()
    {
        MenuControl.Instance.heroMenu.hero.Heal(null, null, 9999);

        for (int ii = 0; ii < 2; ii++)
        {
            List<Card> upgradableCards = new List<Card>();
            foreach (Card card in MenuControl.Instance.heroMenu.cardsOwned)
            {
                if (card.RandomUpgradeCard != null)
                {
                    upgradableCards.Add(card);
                }
            }
            if (upgradableCards.Count > 0)
            {
                Card randomCard = upgradableCards[Random.Range(0, upgradableCards.Count)];
                Card newCard = MenuControl.Instance.heroMenu.UpgradeToRandomCardInDeck(randomCard);
            }
        }
        MenuControl.Instance.dataControl.SaveData();
        CloseEvent();
    }
}

