using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaSack : Ability
{
    public int copies;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        List<Card> eligibleCards = MenuControl.Instance.heroMenu.PathCards();
        eligibleCards.AddRange(MenuControl.Instance.heroMenu.GetUnlockedMinionCards());

        List<Card> niceCards = new List<Card>();
        foreach (Card card in eligibleCards)
        {
            if (card.level == 1 && card is Minion)
            {
                niceCards.Add(card);
            }
        }

        if (niceCards.Count > 0)
        {
            for (int ii = 0; ii < copies; ii += 1)
            {

                Card cardTemplate = niceCards[Random.Range(0, niceCards.Count)];

                Card newCard = GetCard().player.CreateCardInGameFromTemplate(cardTemplate);
                newCard.initialCost = 0;
                newCard.cardTags.Add(MenuControl.Instance.niceTag);
                newCard.DrawThisCard();

            }
        }
    }
}
