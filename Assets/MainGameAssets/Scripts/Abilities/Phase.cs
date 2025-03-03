using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Phase : Ability
{

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        Unit unit1 = (Unit)sourceCard;
        if (unit1 == null || unit1 is LargeHero) return;

        Tile tile1 = unit1.GetTile();
        if (tile1 == null) return;

        Unit unit2 = targetTile.GetUnit();
        if (unit2 == null || unit2 is LargeHero) return;

        Tile tile2 = targetTile;
        if (tile2 == null) return;

        unit1.ChangePosition(unit2);
    }

}
