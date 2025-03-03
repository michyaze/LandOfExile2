using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageModifierImmuneToEffect : DamageModifier
{
    public Effect effectTemplate;
    public bool friendlyMinionsAlsoImmune;

    public override int ModifyAmount(Card sourceCard, Ability ability, Unit unit, int currentAmount)
    {
        if (unit == GetCard())
        {
            if (ability.GetEffect() != null && ability.GetEffect().originalTemplate == effectTemplate)
                return -99999;

        }

        if (friendlyMinionsAlsoImmune)
        {
            if (unit.player == GetCard().player)
            {
                if (ability.GetEffect() != null && ability.GetEffect().originalTemplate == effectTemplate)
                    return -99999;
            }
        }

        return currentAmount;
    }


}
