using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveEffectsOnTileByTemplate : Ability
{
    public List<Effect> templateEffects;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        foreach (var templateEffect in templateEffects)
        {
            // var finalTemplate = templateEffect;
            // if (finalTemplate.originalTemplate)
            // {
            //     finalTemplate = finalTemplate.originalTemplate;
            // }
            foreach (Effect effect in targetTile.GetEffectsWithTemplate(templateEffect).ToArray())
            {
                targetTile.RemoveEffect(sourceCard, this, effect);
            }
        }
    }

}
