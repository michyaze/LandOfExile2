using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerSoftBarrier : Trigger
{
    public Skill softBarrierTalent;
    public Skill softBarrierPlusTalent;

    public override void TurnStarted(Player player)
    {
        if (player == GetCard().player)
        {
            int level = MenuControl.Instance.CountOfCardsInList(softBarrierTalent, MenuControl.Instance.levelUpMenu.variableTalentsAcquired) + (2 * MenuControl.Instance.CountOfCardsInList(softBarrierPlusTalent, MenuControl.Instance.levelUpMenu.variableTalentsAcquired));
            if (level == 0) level = 3;

            if (GetEffect().remainingCharges < level)
                ((Unit)GetCard()).ApplyEffect(GetCard(), this, GetEffect().originalTemplate, 1);
        }
    }
}
