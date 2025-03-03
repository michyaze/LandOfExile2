using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeChargesOfEffectOnUnit : Ability
{
    public Effect templateEffect;
    public int chargesToConsume = 1;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
       
        foreach (Effect effect in targetTile.GetUnit().GetEffectsWithTemplate(templateEffect))
        {
            if (effect.remainingCharges > 0)
                effect.ConsumeCharges(this, chargesToConsume);
        }

    }

}
