using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BondHealing : Ability
{
     

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        int amountToHeal = 0;
        foreach (Tile tile in targetTile.GetAdjacentTilesLinear(1))
        {
            if (tile.GetUnit() != null && tile.GetUnit().player == sourceCard.player)
            {
                amountToHeal += 1;
            }
        }
        if (amountToHeal > 0)
        {
            targetTile.GetUnit().Heal(sourceCard, this, amountToHeal);
        }

    }
}
