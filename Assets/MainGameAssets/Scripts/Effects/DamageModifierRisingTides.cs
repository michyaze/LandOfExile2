using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageModifierRisingTides : DamageModifier
{

    public bool canModify;

    public override int ModifyAmount(Card sourceCard, Ability ability, Unit unit, int currentAmount)
    {
        if (canModify)
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
