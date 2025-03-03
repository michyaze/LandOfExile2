using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commissar : Ability
{
    public List<Effect> effectTemplates = new List<Effect>();
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        Unit unit = targetTile.GetUnit();
        unit.SufferDamage(sourceCard, this, 0, true);

        foreach (Minion minion in sourceCard.player.GetMinionsOnBoard())
        {
            minion.ChangeMoves(this, minion.remainingMoves + 1);
            minion.ChangeActions(this, minion.remainingActions + 1);
        }

    }
}
