using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyEffectsChargesEqualToOtherEffect : Ability
{
    public Effect otherEffectTemplate;

    public List<Effect> templateEffects = new List<Effect>();
    public bool notToSelf;
    public bool alsoApplyToAdjacentSameTeamUnits;
    public bool useTargetCharges = false;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        var card = GetCard();
        Unit target;
        if (card is Unit)
        {
            target = (Unit)GetCard();
        }
        else
        {
            target = card.player.GetHero();
        }
        if (useTargetCharges)
        {
            if (targetTile.GetUnit())
            {
                target = targetTile.GetUnit();
            }
        }
        if (target.GetEffectsWithTemplate(otherEffectTemplate).Count > 0)
        {
            int charges = target.GetEffectsWithTemplate(otherEffectTemplate)[0].remainingCharges;
            if (charges > 0)
            {
                if (!notToSelf)
                {
                    for (int ii = 0; ii < templateEffects.Count; ii += 1)
                    {
                        Effect effectToApply = templateEffects[ii];
                        targetTile.GetUnit().ApplyEffect(sourceCard, this, effectToApply, charges);
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

}
