using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyRandomEffects : Ability
{
    public List<Effect> templateEffects = new List<Effect>();
    public int charges;
    public bool notToSelf;
    public bool alsoApplyToAdjacentSameTeamUnits;
    public bool useAmount = false;
    public bool toHero = false;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        
        if (toHero)
        {
            var selectedEffect = templateEffects.RandomItem();
            //for (int ii = 0; ii < templateEffects.Count; ii += 1)
            {
                Effect effectToApply = selectedEffect;
                
                var effect = sourceCard.player.GetHero()
                    .ApplyEffect(sourceCard, this, effectToApply, useAmount ? amount : charges);
                
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
                       
                    var selectedEffect = templateEffects.RandomItem();
                //for (int ii = 0; ii < templateEffects.Count; ii += 1)
                {
                    Effect effectToApply = selectedEffect;
                    var effect = targetTile.GetUnit().ApplyEffect(sourceCard, this, effectToApply, useAmount?amount:charges);
                    
                } 
                }
            }

            if (alsoApplyToAdjacentSameTeamUnits) {

                foreach (Tile tile in targetTile.GetAdjacentTilesLinear())
                {
                    if (tile.GetUnit() != null && tile.GetUnit().player == targetTile.GetUnit().player)
                    {
                        var selectedEffect = templateEffects.RandomItem();
                        //for (int ii = 0; ii < templateEffects.Count; ii += 1)
                        {
                            Effect effectToApply = selectedEffect;
                            var effect = tile.GetUnit().ApplyEffect(sourceCard, this, effectToApply, useAmount?amount: charges);
                            
                        }
                    }
                }
            }
        }
        
    }

}
