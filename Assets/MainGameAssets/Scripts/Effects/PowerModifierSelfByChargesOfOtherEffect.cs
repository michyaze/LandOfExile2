using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerModifierSelfByChargesOfOtherEffect : PowerModifier
{
    public Effect templateEffect;
    public bool reduceByCharges;

    public override int ModifyPower(Unit unit, int currentPower, Unit attacker, Unit defender)
    {
        int newPower = currentPower;


        if (unit == GetCard() && unit.GetZone() == MenuControl.Instance.battleMenu.board)
        {
            int otherCharges = 0;
            foreach (Effect effect in unit.GetEffectsWithTemplate(templateEffect))
            {
                otherCharges += effect.remainingCharges;
            }
            newPower = Mathf.Max(0, currentPower + (reduceByCharges ? -otherCharges : otherCharges));
        }

        return newPower;
    }
}
