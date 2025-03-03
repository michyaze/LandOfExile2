using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyEffectsChargesEqualToThisEffect : Ability
{
    public List<Effect> templateEffects = new List<Effect>();
    public bool notToSelf;
    public bool alsoApplyToAdjacentSameTeamUnits;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
      
            int charges = GetEffect().remainingCharges;
            if (charges > 0)
            {
                if (!notToSelf)
                {
                    for (int ii = 0; ii < templateEffects.Count; ii += 1)
                    {
                        Effect effectToApply = templateEffects[ii];
                        if (targetTile != null && targetTile.GetUnit() != null)
                        {
                            targetTile.GetUnit().ApplyEffect(sourceCard, this, effectToApply, charges);
                        }
                    }
                }

                if (alsoApplyToAdjacentSameTeamUnits)
                {

                    foreach (Tile tile in targetTile.GetAdjacentTilesLinear())
                    {
                        if (tile.GetUnit() != null && tile.GetUnit().player == targetTile.GetUnit().player)
                        {
                            for (int ii = 0; ii < templateEffects.Count; ii += 1)
                            {
                                Effect effectToApply = templateEffects[ii];
                                tile.GetUnit().ApplyEffect(sourceCard, this, effectToApply, charges);
                            }
                        }
                    }
                }
            }

        
    }

}
