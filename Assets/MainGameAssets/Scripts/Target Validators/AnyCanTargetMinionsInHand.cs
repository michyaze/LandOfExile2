using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyCanTargetMinionsInHand : AnyCanTarget
{
    public int minCount;
    public int maxCount;

    public override bool CanTarget(Card sourceCard, Tile tile)
    {
        if (sourceCard != GetCard()) return true;

        int count = 0;
        foreach (Card card in sourceCard.player.cardsInHand)
        {
            if (card is Minion)
            {
                count += 1;
            }
        }

        if (minCount > 0 && count < minCount) return false;
        if (maxCount > 0 && count > maxCount) return false;

        return true;
    }
}
