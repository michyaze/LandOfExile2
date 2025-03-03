using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAttackType : Ability
{
    public TargetValidator attackTypeValidator;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        Unit unit = targetTile.GetUnit();

        if (unit.activatedAbility != null && unit.activatedAbility is Attack)
        {
            ((Attack)unit.activatedAbility).targetValidator = attackTypeValidator;
        }
    }
}
