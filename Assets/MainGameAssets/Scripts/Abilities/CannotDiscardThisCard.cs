using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannotDiscardThisCard : Ability
{
    public bool preventAutoDiscardAtEndOfTurnOnly;

    public override bool CanDiscard(Card card, bool autoEndTurn)
    {

        if (preventAutoDiscardAtEndOfTurnOnly)
        {
            if (autoEndTurn && GetCard() == card)
                return false;
        }
        else
        {
            return GetCard() != card;
        }

        return true;
    }
}
