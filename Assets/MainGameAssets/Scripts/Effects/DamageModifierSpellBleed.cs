using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageModifierSpellBleed : DamageModifier
{

    public override int ModifyAmount(Card sourceCard, Ability ability, Unit unit, int currentAmount)
    {
        if (unit == GetCard())
        {
            if (ability.GetCard() is Castable && ability.GetCard().cardTags.Contains(MenuControl.Instance.spellTag) && ability.GetCard().player != GetCard().player)
                return currentAmount + remainingCharges;

        }

        return currentAmount;
    }


}
