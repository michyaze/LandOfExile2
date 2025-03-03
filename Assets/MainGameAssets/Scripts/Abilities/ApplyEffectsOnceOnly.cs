using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyEffectsOnceOnly: ApplyEffects 
{
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (targetTile != null && targetTile.GetUnit() != null)
        {
            foreach (Effect templateEffect in templateEffects)
            {
                if (targetTile.GetUnit().GetEffectsWithTemplate(templateEffect).Count == 0)
                    base.PerformAbility(sourceCard, targetTile, amount);
            }
        }
    }

}
