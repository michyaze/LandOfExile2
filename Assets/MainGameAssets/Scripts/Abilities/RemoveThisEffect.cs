using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveThisEffect : Ability
{
    public bool onlyRemoveIfAppliedBefore = false;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (onlyRemoveIfAppliedBefore && GetEffect().lastApplyTurnCount == MenuControl.Instance.battleMenu.currentTurn)
        {
            return;
        }
        ((Unit)GetCard()).RemoveEffect(sourceCard, this, GetEffect());
    }

}
