using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCowering : Ability
{
    public Effect blocktemplate;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        Effect blockEffect = targetTile.GetUnit().GetEffectsWithTemplate(blocktemplate)[0];
        int chargesOfBlock = blockEffect.remainingCharges;
        targetTile.GetUnit().RemoveEffect(GetCard(), this, blockEffect);
        targetTile.GetUnit().ChangePower(this, targetTile.GetUnit().currentPower + (2 * chargesOfBlock));

    }
}
