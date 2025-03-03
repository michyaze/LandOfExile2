using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrainBlockAdjacentAsPower : Ability
{
    public Effect blockEffectTemplate;
    public int powerPerCharge = 1;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        int charges = 0;

        foreach (Tile tile in ((Unit)sourceCard).GetTile().GetAdjacentTilesLinear(1))
        {
            if (tile.GetUnit() != null)
            {
                foreach (Effect effect in tile.GetUnit().GetEffectsWithTemplate(blockEffectTemplate).ToArray())
                {
                    charges += effect.remainingCharges;
                    tile.GetUnit().RemoveEffect(GetCard(),this, effect);
                }
            }
        }

        if (charges > 0)
        {
            ((Unit)GetCard()).ChangePower(this, ((Unit)GetCard()).currentPower + (powerPerCharge * charges));
        }
    }
}
