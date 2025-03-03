using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyEffectsOnTile : Ability
{
    public List<Effect> templateEffects = new List<Effect>();
    public List<int> charges = new List<int>();
    public bool useAmount = false;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        
        
        for (int ii = 0; ii < templateEffects.Count; ii += 1)
        {
            Effect effectToApply = templateEffects[ii];
            var effect = targetTile.ApplyEffect(sourceCard, this, effectToApply, useAmount?amount:charges[ii]);
        }
        
    }

}
