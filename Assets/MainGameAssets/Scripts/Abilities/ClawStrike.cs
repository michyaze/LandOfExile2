using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawStrike : Ability
{
    public int bonusPower;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        Hero hero = sourceCard.player.GetHero();
        int originalPower = hero.currentPower;
        hero.ChangePower(this, hero.currentPower + bonusPower);

        hero.ForceAttack(targetTile);

        hero.ChangePower(this, originalPower);

    }
}
