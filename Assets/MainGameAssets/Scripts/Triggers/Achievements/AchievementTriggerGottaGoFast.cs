using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementTriggerGottaGoFast : TriggerAchievement
{
    public int minMoves = 4;
    public int movesThisTurn;

    public override void UnitMoved(Unit unit, Tile originalTile, Tile destinationTile)
    {
        if (unit == MenuControl.Instance.battleMenu.player1.GetHero())
        {
            movesThisTurn += 1;
            if (movesThisTurn >= minMoves)
            {
                MarkAchievementCompleted();
            }
        }
    }

    public override void TurnStarted(Player player)
    {
        movesThisTurn = 0;
    }
}
