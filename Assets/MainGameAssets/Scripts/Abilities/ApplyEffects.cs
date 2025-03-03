using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyEffects : Ability
{
    public List<Effect> templateEffects = new List<Effect>();
    public List<int> charges = new List<int>();
    public bool notToSelf;
    public bool alsoApplyToAdjacentSameTeamUnits;
    public bool useAmount = false;
    public bool toHero = false;
    public bool addAmount = false;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        
        if (toHero)
        {
            for (int ii = 0; ii < templateEffects.Count; ii += 1)
            {
                Effect effectToApply = templateEffects[ii];
             
                var value = amount;
                if (addAmount)
                {
                    value += charges[ii];
                }
                else if (!useAmount)
                {
                    value = charges[ii];
                }
                
                var effect = sourceCard.player.GetHero()
                    .ApplyEffect(sourceCard, this, effectToApply, value);
                
            }
        }
        else
        {
            if (targetTile == null)
            {
                return;
            }
            if (!notToSelf)
            {
                if (targetTile.GetUnit() != null)
                {
                       
                for (int ii = 0; ii < templateEffects.Count; ii += 1)
                {
                    
                    var value = amount;
                    if (addAmount)
                    {
                        value += charges[ii];
                    }
                    else if (!useAmount)
                    {
                        value = charges[ii];
                    }
                    Effect effectToApply = templateEffects[ii];
                    var effect = targetTile.GetUnit().ApplyEffect(sourceCard, this, effectToApply, value);
                    
                } 
                }
            }

            if (alsoApplyToAdjacentSameTeamUnits) {

                foreach (Tile tile in targetTile.GetAdjacentTilesLinear())
                {
                    if (tile.GetUnit() != null && tile.GetUnit().player == targetTile.GetUnit().player)
                    {
                        for (int ii = 0; ii < templateEffects.Count; ii += 1)
                        {
                            
                            var value = amount;
                            if (addAmount)
                            {
                                value += charges[ii];
                            }
                            else if (!useAmount)
                            {
                                value = charges[ii];
                            }
                            Effect effectToApply = templateEffects[ii];
                            var effect = tile.GetUnit().ApplyEffect(sourceCard, this, effectToApply, value);
                            
                        }
                    }
                }
            }
        }
        
    }

}
