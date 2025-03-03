using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttackTargetUnit : Trigger
{
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        Hero hero = sourceCard.player.GetHero();
        if (hero.activatedAbility != null && hero.activatedAbility is Attack)
            hero.ForceAttack(targetTile);
    }
}
