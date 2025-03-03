using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceEffectByTemplate : Ability
{
    public Effect templateEffect;
    public int charges;
    public bool toHero = false;
    public bool useAmount = false;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        if (toHero)
        {
            var hero = sourceCard.player.GetHero();
            foreach (Effect effect in hero.GetEffectsWithTemplate(templateEffect).ToArray())
            {
                hero.ReduceEffect(sourceCard, this, effect,useAmount?amount:charges);
            }

            return;
        }
        foreach (Effect effect in targetTile.GetUnit().GetEffectsWithTemplate(templateEffect).ToArray())
        {
            targetTile.GetUnit().ReduceEffect(sourceCard, this, effect,useAmount?amount:charges);
        }
    }

}
