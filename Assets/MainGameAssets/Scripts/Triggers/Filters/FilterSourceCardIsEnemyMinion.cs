using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterSourceCardIsEnemyMinion: TriggerFilter
{
    
    public override bool Check(Card sourceCard, Tile targetTile, int amount = 0)
    {

        return (sourceCard.player != GetCard().player && sourceCard is Minion);

    }
}
