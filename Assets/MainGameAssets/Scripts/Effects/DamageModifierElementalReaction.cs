using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageModifierElementalReaction : DamageModifier
{
    public int modifierAmount = 1;
    public override int ModifyAmount(Card sourceCard, Ability ability, Unit unit, int currentAmount)
    {
        DirectDamage damageAbility = ability as DirectDamage;
        Card card = GetCard();
        if (card != null && sourceCard != null && card.player == sourceCard.player && damageAbility &&
            damageAbility.isElementalReaction)
        {
            return currentAmount + modifierAmount * (GetComponent<Effect>().chargesStack? GetComponent<Effect>().remainingCharges:1);
        }

        return currentAmount;
    }


}
