using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SculptSelf : Ability
{
    public Minion minionToSummonTemplate;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        Unit unit = targetTile.GetUnit();
        Hero hero = unit.player.GetHero();

        unit.ExhaustThisCard();
        Minion minion = (Minion)hero.player.CreateCardInGameFromTemplate(minionToSummonTemplate);
        minion.TargetTile(targetTile, false);

        minion.ChangeMoves(this, hero.remainingMoves);
        minion.ChangeActions(this, hero.remainingActions);
        minion.ChangePower(this, hero.currentPower);
        minion.ChangeCurrentHP(this, hero.currentHP);

    }

}
