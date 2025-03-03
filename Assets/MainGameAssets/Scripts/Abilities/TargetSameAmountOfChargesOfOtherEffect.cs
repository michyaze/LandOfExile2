using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetSelectType
{
    Self,
    Source,
    TargetTile,
}
public class TargetSameAmountOfChargesOfOtherEffect : Ability
{
    public Effect otherEffectTemplate;
    public Ability otherAbility;
    public TargetSelectType targetSelectType;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        Unit target = null;
        if (targetSelectType == TargetSelectType.Self)
        {
            target = GetCard() as Unit;
        }
        else if (targetSelectType == TargetSelectType.Source)
        {
            target = sourceCard as Unit;
        }
        else if (targetSelectType == TargetSelectType.TargetTile)
        {
            target = targetTile.GetUnit();
        }

        if (target == null)
        {
            return;
        }

        if (target.GetEffectsWithTemplate(otherEffectTemplate).Count > 0)
        {
            int charges = target.GetEffectsWithTemplate(otherEffectTemplate)[0].remainingCharges;
            if (charges > 0)
            {

                if (targetTile.GetUnit() != null)
                {
                    otherAbility.PerformAbility(sourceCard, targetTile, charges);
                }
            }
        }
    }

}
