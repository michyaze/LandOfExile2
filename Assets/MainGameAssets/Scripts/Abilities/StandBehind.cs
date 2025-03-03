using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandBehind : Ability
{
    public Ability changePowerHPAbility;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        int count = sourceCard.player.cardsInHand.Count;
        foreach (Card card in sourceCard.player.cardsInHand.ToArray())
        {
            card.DiscardThisCard();
        }

        for (int ii = 0; ii < count; ii += 1)
        {
            changePowerHPAbility.PerformAbility(sourceCard, targetTile, 0);
        }
    }
}
