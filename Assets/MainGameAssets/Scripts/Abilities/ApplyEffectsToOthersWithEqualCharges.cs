using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyEffectsToOthersWithEqualCharges : Ability
{
    public List<Effect> originalEffects= new List<Effect>();

    public List<Effect> targetEffects = new List<Effect>();
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        var target = targetTile.GetUnit();
        if (target == null)
        {
            return;
        }

        var sourceUnit = sourceCard as Unit;
        if (sourceUnit == null)
        {
            return;
        }

        for (int i = 0; i < originalEffects.Count; i++)
        {
            var originEffect = originalEffects[i];


            if (sourceUnit.GetEffectsWithTemplate(originEffect).Count > 0)
            {
                var chargeCount = sourceUnit.GetEffectsWithTemplate(originEffect)[0].remainingCharges;
                var targetEffect = originEffect;
                if (targetEffects.Count > i)
                {
                    targetEffect = targetEffects[i];
                }
            
                target.ApplyEffect(sourceCard, this, targetEffect, chargeCount);
            }
            
        }
    }

}
