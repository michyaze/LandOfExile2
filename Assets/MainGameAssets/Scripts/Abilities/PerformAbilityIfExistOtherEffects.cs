using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformAbilityIfExistOtherEffects : Ability
{
    public List<Effect> otherEffectsTemplate;

    public Ability ability;
    public Ability abilityOnSource;
    public Ability elseAbility;
    public Ability elseAbilityOnSource;

    public bool checkTileEffect = false;
    // public bool notToSelf;
    // public bool alsoApplyToAdjacentSameTeamUnits;
     public bool useTargetCharges = false;

     public bool isOppositeOnEffectCheck = false;

     public bool checkSource = false;
    public override bool CanTargetTile(Card card, Tile targetTile)
    {
        if (GetTargetValidator() != null)
        {
            if (!base.CanTargetTile(card, targetTile))
            {
                return false;
            }
        }

        if (elseAbility != null || elseAbilityOnSource!=null)
        {
            return true;
        }
        
        var existOtherEffects = false;
        if (checkTileEffect)
        {
            foreach (var otherEffectTemplate in otherEffectsTemplate)
            {
                if (targetTile.GetEffectsWithTemplate(otherEffectTemplate).Count > 0)
                {
                    if (isOppositeOnEffectCheck)
                    {
                        return false;
                    }
                    return true;
                }
            }
        }
        else
        {
            var target = targetTile.GetUnit();
            if (checkSource)
            {
                target = card as Unit;
            }
            else
            {
                
            }
            if (!target)
            {
                return false;
            }
            foreach (var otherEffectTemplate in otherEffectsTemplate)
            {
                if (target.GetEffectsWithTemplate(otherEffectTemplate).Count > 0)
                {
                    if (isOppositeOnEffectCheck)
                    {
                        return false;
                    }
                    return true;
                }
            }
        }
        

        if (isOppositeOnEffectCheck)
        {
            return true;
        }
        return false;
    }

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        if (!CanTargetTile(sourceCard, targetTile))
        {
            return;
        }

        var existOtherEffects = false;
        var effectsCount = 0;
        if (checkTileEffect)
        {
            foreach (var otherEffectTemplate in otherEffectsTemplate)
            {
                var count = targetTile.GetEffectsWithTemplate(otherEffectTemplate).Count;
                if (count > 0)
                {
                    existOtherEffects = true;
                    effectsCount += targetTile.GetEffectsWithTemplate(otherEffectTemplate)[0].remainingCharges;
                    break;
                }
            }
        }
        else
        {
            var target = targetTile.GetUnit();
            if (checkSource)
            {
                target = sourceCard as Unit;
            }
            if (!target)
            {
                return;
            }
            foreach (var otherEffectTemplate in otherEffectsTemplate)
            {
                var count = target.GetEffectsWithTemplate(otherEffectTemplate).Count;
                if (count > 0)
                {
                    
                    effectsCount += target.GetEffectsWithTemplate(otherEffectTemplate)[0].remainingCharges;
                    existOtherEffects = true;
                    break;
                }
            }
        }
        
        if ((existOtherEffects && !isOppositeOnEffectCheck) || (!existOtherEffects && isOppositeOnEffectCheck))
        {
            if (ability)
            {
                ability.PerformAbility(sourceCard,targetTile,useTargetCharges?effectsCount:amount);

            }

            if(sourceCard is Unit unit)
            {
                abilityOnSource?.PerformAbility(sourceCard,unit.GetTile(),amount);
            }
        }
        else
        {
            if (elseAbility)
            {
                elseAbility?.PerformAbility(sourceCard,targetTile,useTargetCharges?effectsCount:amount);

            }

            if(sourceCard is Unit unit)
            {
                elseAbilityOnSource?.PerformAbility(sourceCard,unit.GetTile(),amount);
            }
        }
    }

}
