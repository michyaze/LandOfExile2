using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRecruitmentContract : Trigger
{
    public int minionsToDraw;
    public int numberOfFriendlyAccomplices;
    public Effect accompliceTemplate;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        for (int ii = 0; ii < minionsToDraw; ii += 1)
        {
            foreach (Card card in GetCard().player.cardsInDeck)
            {
                if (card is Minion)
                {
                    card.DrawThisCard();
                    break;
                }
            }
        }
    }


    public override void CardDiscarded(Card card, bool automaticDiscard)
    {
        if (card == GetCard() && !automaticDiscard)
        {
            List<Minion> otherMinions = new List<Minion>();
            foreach (Minion minion in GetCard().player.GetMinionsOnBoard())
            {
                if (minion.GetComponentsInChildren<TriggerAccomplice>().Length == 0)
                {
                    otherMinions.Add(minion);
                }
            }

            if (otherMinions.Count > 0)
            {
                otherMinions.Shuffle();
                for (int ii = 0; ii < numberOfFriendlyAccomplices; ii += 1)
                {
                    Unit unit = otherMinions[ii];
                    unit.ApplyEffect(GetCard(), this, accompliceTemplate, 0);
                }

            }
        }
    }
}
