using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSwallowedWhole : Trigger
{
    public Card cardToDraw;

    public override void MinionDestroyed(Card sourceCard, Ability ability, int damageAmount, Minion minion)
    {
        if (minion == GetCard())
        {
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                cardToDraw.DrawThisCard();
            });
        }
    }
}
