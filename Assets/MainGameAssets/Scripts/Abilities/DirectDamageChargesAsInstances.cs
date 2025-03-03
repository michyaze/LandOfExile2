using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectDamageChargesAsInstances : Ability
{
    public int damageAmountPerInstance = 1;

    public bool exhaustTargetIfDestroyed;


    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        //Perform standard attack
        if (targetTile.GetUnit() != null)
        {
            Unit unit = targetTile.GetUnit();

            int instances = GetEffect().remainingCharges;

            for (int ii = 0; ii < instances; ii += 1)
            {
                unit.SufferDamage(sourceCard, this, damageAmountPerInstance);
            }

            if (exhaustTargetIfDestroyed && unit.GetZone() != MenuControl.Instance.battleMenu.board)
            {
                unit.ExhaustThisCard();
            }
        }

    }


}
