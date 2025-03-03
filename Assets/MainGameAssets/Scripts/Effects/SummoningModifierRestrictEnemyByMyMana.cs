using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummoningModifierRestrictEnemyByMyMana :SummoningModifier 
{
    public int summonsThisTurn = 0;

    public override bool CanSummon(Unit unit, Tile tile)
    {
        if (unit.player != GetCard().player)
        {
            if (summonsThisTurn >= GetCard().player.GetCurrentMana())
            {
                return false;
            }
        }

        return true;
    }

    public override void MinionSummoned(Minion minion)
    {
        if (minion.player != GetCard().player)
        {
            summonsThisTurn += 1;
        }
    }

    public override void TurnEnded(Player player)
    {
        if (GetCard().player != player)
        {
            summonsThisTurn = 0;
        }
    }
}
