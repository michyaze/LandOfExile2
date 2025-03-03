using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageModifierImmuneExceptFromUnitsWithEffect : DamageModifier
{
    public Effect templateEffect;
    public override int ModifyAmount(Card sourceCard, Ability ability, Unit unit, int currentAmount)
    {
        if (unit == GetCard())
        {
            if (ability.GetCard() is Unit && ability is Attack)
            {
                Unit attacker = (Unit)ability.GetCard();
                if (attacker.GetEffectsWithTemplate(templateEffect).Count > 0)
                    return currentAmount;

            }
        }
        else
        {
            return currentAmount;
        }

        return -99999;
    }


}
