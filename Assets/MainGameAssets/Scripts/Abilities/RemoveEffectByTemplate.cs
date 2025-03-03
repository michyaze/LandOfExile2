using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveEffectByTemplate : Ability
{
    public Effect templateEffect;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        foreach (Effect effect in targetTile.GetUnit().GetEffectsWithTemplate(templateEffect).ToArray())
        {
            targetTile.GetUnit().RemoveEffect(sourceCard, this, effect);
        }
    }

}
