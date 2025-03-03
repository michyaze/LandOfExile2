using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerModifier : Effect
{
    public virtual int ModifyPower(Unit unit, int currentPower, Unit attacker, Unit defender)
    {
        return currentPower;
    }
}
