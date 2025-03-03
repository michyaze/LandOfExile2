using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageModifierPinchCritter : DamageModifier
{

    public override int ModifyAmount(Card sourceCard, Ability ability, Unit unit, int currentAmount)
    {
        if (GetCard().player.GetHero().GetHP() < Mathf.CeilToInt(GetCard().player.GetHero().GetInitialHP() / 2f))
        {
            if (sourceCard != null && sourceCard is Castable && sourceCard.cardTags.Contains(MenuControl.Instance.spellTag) && sourceCard.player == GetCard().player)
            {

                return currentAmount + remainingCharges;

            }
            else if (ability != null && ability.GetCard() is Castable && ability.GetCard().cardTags.Contains(MenuControl.Instance.spellTag) && ability.GetCard().player == GetCard().player)
            {
                return currentAmount + remainingCharges;

            }
        }

        return currentAmount;
    }
}
