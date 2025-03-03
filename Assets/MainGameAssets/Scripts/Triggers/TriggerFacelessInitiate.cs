using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFacelessInitiate : Trigger
{
    public override void MinionDestroyed(Card sourceCard, Ability ability, int damageAmount, Minion minion)
    {
        if (ability == GetCard().activatedAbility)
        {
            minion.Sacrifice(GetCard(), this);
        }
    }
}
