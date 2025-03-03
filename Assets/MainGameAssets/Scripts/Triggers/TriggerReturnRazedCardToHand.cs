using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerReturnRazedCardToHand : Trigger
{
    public override void CardRazed(Card card)
    {

        if (!(GetCard() is NewWeapon) && (!(GetCard() is Unit) || GetCard().GetZone() != MenuControl.Instance.battleMenu.board)) return;

        MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
        {
            card.PutIntoZone(MenuControl.Instance.battleMenu.hand);
        });
    }
}
