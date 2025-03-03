using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMyHeroToTile : Ability
{
    public bool noMoveTrigger;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        Hero hero = sourceCard.player.GetHero();
        if (targetTile.isMoveable() || targetTile.GetUnit() == hero)
        {
            if (noMoveTrigger)
            {
                hero.MoveToTile(targetTile);
            }
            else
            {
                hero.TargetTile(targetTile, false);
            }
        }
    }
}
