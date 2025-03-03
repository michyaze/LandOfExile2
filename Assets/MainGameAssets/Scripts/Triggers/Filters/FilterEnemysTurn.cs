using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterEnemysTurn : TriggerFilter
{

    public override bool Check(Card sourceCard, Tile targetTile, int amount = 0)
    {
        return (GetCard().player != MenuControl.Instance.battleMenu.currentPlayer);

    }
}
