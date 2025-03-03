using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExhaustVent : Ability
{

    public int damageAmount;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        Unit thisUnit = (Unit)GetCard();

        for (int ii = 0; ii < MenuControl.Instance.battleMenu.boardMenu.totalActiveTiles / 4; ii += 1)
        {
            if (thisUnit.GetTile().GetTileLeft(ii) != null && thisUnit.GetTile().GetTileLeft(ii).GetUnit() != null)
            {
                Unit unit = thisUnit.GetTile().GetTileLeft(ii).GetUnit();
                unit.SufferDamage(sourceCard, this, damageAmount);
                return;
            }

            if (thisUnit.GetTile().GetTileRight(ii) != null && thisUnit.GetTile().GetTileRight(ii).GetUnit() != null)
            {
                Unit unit = thisUnit.GetTile().GetTileRight(ii).GetUnit();
                unit.SufferDamage(sourceCard, this, damageAmount);
                return;
            }
        }
    }
}
