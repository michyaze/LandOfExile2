using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTimeModifierWithEffect : DamageTimeModifier
{
    public Effect effectTemplate;
    public bool shouldRemoveEffect;
    public  int overrideRemainingCharges;
    public override int ModifyAmount(Card sourceCard, Ability ability, Unit unit, int currentAmount)
    {
        if (!GetCard() || sourceCard == null || unit == null)
        {
            return 0;
        }
        if (GetCard().player != sourceCard.player)
        {
            return 0;
        }
        var res = 0;
        //if (GetCard().player.GetHero().GetHP() < Mathf.CeilToInt(GetCard().player.GetHero().GetBaseHP() / 2f))
        if(unit.GetEffectsWithTemplate(effectTemplate).Count>0)
        {
            if (sourceCard != null && sourceCard is Castable && sourceCard.cardTags.Contains(MenuControl.Instance.spellTag) && sourceCard.player == GetCard().player)
            {

                res = overrideRemainingCharges;

            }
            else if (ability != null && ability.GetCard() is Castable && ability.GetCard().cardTags.Contains(MenuControl.Instance.spellTag) && ability.GetCard().player == GetCard().player)
            {
                res = overrideRemainingCharges;

            }
        }

        if (res!=0 && shouldRemoveEffect)
        {
            unit.RemoveEffectByTemplate(GetCard(),null,effectTemplate);
        }

        return res;
    }
}
