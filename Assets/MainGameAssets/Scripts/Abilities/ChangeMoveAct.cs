using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMoveAct : Ability
{
    public int extraActions;
    public int extraMoves;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        targetTile.GetUnit().ChangeMoves( this, targetTile.GetUnit().remainingMoves + extraMoves);
        targetTile.GetUnit().ChangeActions(this, targetTile.GetUnit().remainingActions + extraActions);
    }
}
