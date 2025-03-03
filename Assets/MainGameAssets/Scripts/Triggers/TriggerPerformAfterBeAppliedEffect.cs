using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPerformAfterBeAppliedEffect : Trigger
{
    // 被施加了effect后触发
    public Ability otherAbility;
    public List<Effect> effectTemplates;
    public bool eventMustTriggerOnThisCard = true;
public bool useHeroIfNoSourceUnit = false;
public bool checkValidation = false;
    public override void UnitAppliedEffect(Unit unit, Ability ability, Effect effect, int charges)
    {
        var sourceUnit = GetCard() as Unit;
        
        if (sourceUnit == null && useHeroIfNoSourceUnit)
        {
            sourceUnit = GetCard().player.GetHero();
        }
        if (checkValidation && sourceUnit)
        {
            if (!targetValidator.CanUnitTargetTile(sourceUnit, unit.GetTile()))
            {
                return;
            }
        }
        
        if (sourceUnit && GetTargetValidator() && !GetTargetValidator().CanUnitTargetTile(sourceUnit, unit.GetTile()))
        {
            return;
        }
        if (eventMustTriggerOnThisCard)
        {
            if (unit != GetCard())
            {
                return;
            }
        }

        foreach (var effectTemplate in effectTemplates)
        {
            if (effect.originalTemplate == effectTemplate)
            {
                otherAbility.PerformAbility(GetCard(),unit.GetTile(),charges);
                return;
            }
        }
    }
}
