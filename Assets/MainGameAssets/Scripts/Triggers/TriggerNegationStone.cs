using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNegationStone : Trigger
{
    public override void CardDrawn(Card card)
    {
        if (card == GetCard())
        {
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                card.player.ChangeMana(-1);
            });
        }
    }

    public override bool CanDiscard(Card card, bool autoEndTurn)
    {
        return card != GetCard();
    }

    public override void TurnEnded(Player player)
    {
        if (player == GetCard().player && GetCard().GetZone() == MenuControl.Instance.battleMenu.hand)
        {
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                GetCard().DiscardThisCard();
            });
        }
    }
}
