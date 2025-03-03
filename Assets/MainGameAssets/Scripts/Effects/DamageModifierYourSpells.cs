using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageModifierYourSpells : DamageModifier
{
    public int modifier;
    public bool useChargesInstead;

    public override int ModifyAmount(Card sourceCard, Ability ability, Unit unit, int currentAmount)
    {

        if (ability != null)
        {
            DirectDamage damageAbility = ability as DirectDamage;
            if (damageAbility!=null && damageAbility.isElementalReaction)
            {
                return currentAmount;
            }
        }
        if (sourceCard != null && sourceCard is Castable && sourceCard.cardTags.Contains(MenuControl.Instance.spellTag) && sourceCard.player == GetCard().player)
        {
            if (useChargesInstead) return currentAmount + remainingCharges;
            return currentAmount + modifier;
        }
        else if (ability != null && ability.GetCard() is Castable && ability.GetCard().cardTags.Contains(MenuControl.Instance.spellTag) && ability.GetCard().player == GetCard().player)
        {
            if (useChargesInstead) return currentAmount + remainingCharges;
            return currentAmount + modifier;
        }

        return currentAmount;
    }
}
