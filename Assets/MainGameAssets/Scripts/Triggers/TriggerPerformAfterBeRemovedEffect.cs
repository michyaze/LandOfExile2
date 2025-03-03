using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPerformAfterBeRemovedEffect : Trigger
{
    // 被移除了effect后触发
    public Ability otherAbility;
    public List<Effect> effectTemplates;
    public bool eventMustTriggerOnThisCard = true;

    public override void UnitRemovedEffect(Unit unit, Ability ability, Effect effect)
    {

        var sourceUnit = GetCard() as Unit;
        if (sourceUnit && targetValidator && !targetValidator.CanUnitTargetTile(sourceUnit, unit.GetTile()))
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
                otherAbility.PerformAbility(GetCard(),unit.GetTile());
                return;
            }
        }
    }
}
