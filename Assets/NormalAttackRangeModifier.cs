using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttackRangeModifier : AttackRangeModifier
{
    public List<TargetValidator> normalRangeTargetValidators = new List<TargetValidator>();
    public List<TargetValidator> arcingTargetValidators = new List<TargetValidator>();
    
    public override TargetValidator ModifyRange(Ability ability, TargetValidator currentValidator)
    {

        if ((ability is Attack || ability is AttackOrAid )&& ability.GetCard() == GetCard())
        {
            int amountToAdjust = remainingCharges;
            int index = normalRangeTargetValidators.IndexOf(currentValidator);
            if (index > -1)
            {
                int finalIndex = Mathf.Clamp(index + amountToAdjust, 0, normalRangeTargetValidators.Count - 1);
               return normalRangeTargetValidators[finalIndex];
            }
            index = arcingTargetValidators.IndexOf(currentValidator);
            if (index > -1)
            {
                int finalIndex = Mathf.Clamp(index + amountToAdjust, 0, arcingTargetValidators.Count - 1);
                return arcingTargetValidators[finalIndex];
            }
        }
        
        return currentValidator;
    }
}
