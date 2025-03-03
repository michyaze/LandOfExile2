using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCeremonialist : Trigger
{
    public override void MinionDestroyed(Card sourceCard, Ability ability, int damageAmount, Minion minion)
    {
        if (GetCard().GetZone() == MenuControl.Instance.battleMenu.board && ability.GetCard() == GetCard())
        {
            foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
            {
                try
                {
                    trigger.MinionSacrificed(sourceCard, ability, minion);
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }
    }
}
