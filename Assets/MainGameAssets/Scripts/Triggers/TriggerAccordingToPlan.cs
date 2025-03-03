using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAccordingToPlan : Trigger
{

    public override void MinionDestroyed(Card sourceCard, Ability ability, int damageAmount, Minion minion)
    {
        if (minion.GetComponentsInChildren<TriggerAccomplice>().Length > 0)
        {
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard()
                , GetEffect(), () =>
                {
                    GetCard().player.ChangeMana(1);
                });
        }
    }
}
