using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSleightOfHand : Trigger
{

    public int multistrikeAmount;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (GetCard().player.cardsInDiscard.Count > 0)
        {
            GetCard().player.cardsInDiscard[GetCard().player.cardsInDiscard.Count - 1].DrawThisCard();
        }
    }

    public override void CardDiscarded(Card card, bool automaticDiscard)
    {
        if (!automaticDiscard && card == GetCard())
        {
            GetCard().player.GetHero().remainingActions += multistrikeAmount;
        }
    }
}
