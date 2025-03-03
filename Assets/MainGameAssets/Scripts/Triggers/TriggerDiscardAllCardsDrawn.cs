using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDiscardAllCardsDrawn : Trigger
{
    public override void CardDrawn(Card card)
    {
        if (card.player == GetCard().player)
        {
            if (GetEffect() != null && !GetEffect().enabled) return;

            card.PutIntoZone(MenuControl.Instance.battleMenu.discard);
        }
    }
}
