using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flurry : Ability
{
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        Unit unit = targetTile.GetUnit();

        if (unit != null)
        {
            int initialActions = unit.remainingActions;
            foreach (Unit otherUnit in MenuControl.Instance.battleMenu.GetAllUnitsOnBoard())
            {
                if (otherUnit.player != unit.player)
                {
                    unit.remainingActions = 1;
                    if (!(otherUnit is LargeHero))
                    {
                        if (unit.CanTarget(otherUnit.GetTile()) && unit.activatedAbility is Attack)
                        {
                            unit.ForceAttack(otherUnit.GetTile(), false);
                        }
                    }
                    else
                    {
                        foreach (Tile tile in ((LargeHero)otherUnit).GetTiles())
                        {
                            if (unit.CanTarget(tile) && unit.activatedAbility is Attack)
                            {
                                unit.ForceAttack(tile, false);
                                break;
                            }
                        }
                    }
                }
            }

            unit.remainingActions = initialActions;
        }

    }
}
