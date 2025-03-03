using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRemoveThisEffectEOT : Trigger
{
    public override void TurnEnded(Player player)
    {
        if (player == GetCard().player)
        {
            ((Unit)GetCard()).RemoveEffect(GetCard(), this, GetEffect());
        }
    }
}
