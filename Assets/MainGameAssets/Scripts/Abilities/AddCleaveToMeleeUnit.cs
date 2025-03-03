using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCleaveToMeleeUnit : Ability
{
    //public TargetValidator meleeAttack;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (((Unit)GetCard()).activatedAbility is Attack)
        {
            Attack attackAbility = (Attack)((Unit)GetCard()).activatedAbility;
            if (attackAbility.GetTargetValidator() != null && attackAbility.GetTargetValidator() is TargetLinear targetLinear
                && targetLinear.range <= 1)
            {
                GameObject parentOfAttackAbility = ((Unit)GetCard()).activatedAbility.gameObject;
                Cleave ability = parentOfAttackAbility.AddComponent<Cleave>();
                attackAbility.otherAbilitiesToPerform.Add(ability);
            }
        }
    }
}
