using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageModifierOverCharge : DamageModifier
{
    public float multiplier = 2f;
    public bool canTrigger;

    public override int ModifyAmount(Card sourceCard, Ability ability, Unit unit, int currentAmount)
    {
        if (canTrigger && ability != null && ability.GetCard() is Castable && ability.GetCard().cardTags.Contains(MenuControl.Instance.spellTag) && ability.GetCard().player == GetCard().player)
        {

            return Mathf.FloorToInt(currentAmount * multiplier);
        }

        return currentAmount;
    }
}
