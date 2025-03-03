using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveEffectsByTemplate : Ability
{
    public List<Effect> templateEffects;

    
    
    public override bool CanTargetTile(Card card, Tile tile)
    {
        if (!tile.GetUnit())
        {
            return false;
        }
        foreach (var templateEffect in templateEffects)
        {
            var currentEffects = tile.GetUnit().GetEffectsWithTemplate(templateEffect);
            foreach (var effect in currentEffects)
            {
                if (effect.IsEffectPersistent())
                {
                    return false;
                }
            }
        }

        return true;
        //check does not have 
        //return base.CanTargetTile(card, tile);
    }

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (!targetTile.GetUnit())
        {
            return;
        }
        if (!CanTargetTile( sourceCard, targetTile))
        {
            return;
        }
        foreach (var templateEffect in templateEffects)
        {
            // var realTemplate = templateEffect;
            // if (templateEffect.originalTemplate)
            // {
            //     realTemplate = templateEffect.originalTemplate;
            // }
            foreach (Effect effect in targetTile.GetUnit().GetEffectsWithTemplate(templateEffect).ToArray())
            {
                targetTile.GetUnit().RemoveEffect(sourceCard, this, effect);
            }
        }
    }

}
