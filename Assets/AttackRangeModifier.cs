using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeModifier : Effect
{
    public virtual TargetValidator ModifyRange(Ability ability, TargetValidator currentValidator)
    {
        return currentValidator;
    }
}
