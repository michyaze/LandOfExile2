using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRandomly : Ability
{
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        base.PerformAbility(sourceCard, targetTile, amount);
        var unit = targetTile.GetUnit();
        if (unit)
        {
            //while (unit.remainingMoves > 0)
            {
                bool moved =unit.MoveRandomly();
                if (!moved)
                {
                    return;
                }
            }
        }
    }
}
