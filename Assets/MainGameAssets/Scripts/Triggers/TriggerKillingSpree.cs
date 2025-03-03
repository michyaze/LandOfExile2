using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerKillingSpree : Trigger
{
    public Ability otherAbilityToPerform;

    public override void MinionDestroyed(Card sourceCard, Ability ability, int damageAmount, Minion minion)
    {

        if (GetCard().GetZone() == MenuControl.Instance.battleMenu.board)
        {
            if (sourceCard is Unit && sourceCard.player == GetCard().player)
            {
                MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                {
                    otherAbilityToPerform.PerformAbility(GetCard(), ((Unit)sourceCard).GetTile(), 0);
                });
            }
        }
    }
}
