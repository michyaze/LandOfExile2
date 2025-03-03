using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnsummonMinion : Ability
{
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (targetTile == null)
        {
            return;
        }
        Unit unit = targetTile.GetUnit();
        unit.PutIntoZone(MenuControl.Instance.battleMenu.hand);
        unit.InitializeUnit(false);
    }
}
