using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterAfterTurnX : TriggerFilter
{
    public int onOrAfterRound = 2;
    public int goSecondAdjustment = 0;

    public override bool Check(Card sourceCard, Tile targetTile, int amount = 0)
    {
        int adjustment = 0;
        if (MenuControl.Instance.battleMenu.Player1Starts && sourceCard.player != MenuControl.Instance.battleMenu.player1)
        {
            adjustment = goSecondAdjustment;
        }
        else if (!MenuControl.Instance.battleMenu.Player1Starts && sourceCard.player == MenuControl.Instance.battleMenu.player1)
        {
            adjustment = goSecondAdjustment;
        }

        return (MenuControl.Instance.battleMenu.currentRound >= onOrAfterRound + adjustment);

    }
}
