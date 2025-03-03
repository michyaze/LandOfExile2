using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOrAid : Attack
{
    public Ability friendlyTargetAbility;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (targetTile.GetUnit().player == sourceCard.player)
        {
            if (friendlyTargetAbility)
                friendlyTargetAbility.PerformAbility(sourceCard, targetTile, amount);
            else
            {
                base.PerformAbility(sourceCard, targetTile, amount);
            }
        }
        else
        {
            base.PerformAbility(sourceCard, targetTile, amount);
        }
    }
}
