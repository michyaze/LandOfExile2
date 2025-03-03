using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banish : Ability
{

    public Effect banishedReturnEffectTemplate;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        Minion minionBanished = (Minion)targetTile.GetUnit();
        minionBanished.RemoveFromGame();

        Hero hero = sourceCard.player.GetHero();
        Effect banishedReturnEffect = hero.ApplyEffect(sourceCard, this, banishedReturnEffectTemplate, 0);

        if (banishedReturnEffect == null)
        {
            return;
        }

        banishedReturnEffect.GetComponent<BanishedReturn>().banishedMinion = minionBanished;
        banishedReturnEffect.GetComponent<BanishedReturn>().originalTile = targetTile;

    }
}
