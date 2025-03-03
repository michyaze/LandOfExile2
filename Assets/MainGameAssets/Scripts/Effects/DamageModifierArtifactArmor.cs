using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageModifierArtifactArmor : DamageModifier
{
    public int modifier;

    public override int ModifyAmount(Card sourceCard, Ability ability, Unit unit, int currentAmount)
    {
        if (unit == GetCard().player.GetHero() && ((Artifact)GetCard()).currentCoolDown == 0 && currentAmount > 0)
        {
            ((Artifact)GetCard()).currentCoolDown = ((Artifact)GetCard()).initialCoolDown;
            return currentAmount + modifier;
        }

        return currentAmount;
    }


}
