using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapPositionsWithMyHero : Ability
{


    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        Unit unit = targetTile.GetUnit();
        Hero hero = sourceCard.player.GetHero();
        Tile heroTile = hero.GetTile();

        unit.ChangePosition(hero);
    }
}
