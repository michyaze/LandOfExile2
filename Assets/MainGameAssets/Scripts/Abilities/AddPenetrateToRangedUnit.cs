using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPenetrateToRangedUnit : Ability
{
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (((Unit)GetCard()).activatedAbility is Attack)
        {
            Attack attackAbility = (Attack)((Unit)GetCard()).activatedAbility;
            if (attackAbility.GetTargetValidator() != null && attackAbility.GetTargetValidator() is TargetLinear && ((TargetLinear)attackAbility.GetTargetValidator()).range > 1)
            {
                GameObject parentOfAttackAbility = ((Unit)GetCard()).activatedAbility.gameObject;
                if (parentOfAttackAbility.GetComponent<Penetrate>() == null)
                {
                    Penetrate ability = parentOfAttackAbility.AddComponent<Penetrate>();

                    attackAbility.otherAbilitiesToPerform.Add(ability);
                }
            }
        }
    }
}
