using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRange : Ability
{
    public List<TargetValidator> normalRangeTargetValidators = new List<TargetValidator>();
    public List<TargetValidator> arcingTargetValidators = new List<TargetValidator>();

    public int amountToAdd;
    public bool addEffectCharges;
    public bool useAmount = false;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        Unit unit = targetTile.GetUnit();
        if (unit.activatedAbility != null)
        {
            TargetValidator original = unit.activatedAbility.targetValidator;
            if (original != null)
            {
                int amountToAdjust =useAmount?amount: amountToAdd;
                if (addEffectCharges) amountToAdjust += GetEffect().remainingCharges;

                int index = normalRangeTargetValidators.IndexOf(original);
                if (index > -1)
                {
                    int finalIndex = Mathf.Clamp(index + amountToAdjust, 0, normalRangeTargetValidators.Count - 1);
                    unit.activatedAbility.targetValidator = normalRangeTargetValidators[finalIndex];
                }

                index = arcingTargetValidators.IndexOf(original);
                if (index > -1)
                {
                    int finalIndex = Mathf.Clamp(index + amountToAdjust, 0, arcingTargetValidators.Count - 1);
                    unit.activatedAbility.targetValidator = arcingTargetValidators[finalIndex];
                }
            }
        }
    }
}
