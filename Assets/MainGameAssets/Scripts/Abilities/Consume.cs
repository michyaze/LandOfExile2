using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consume : Ability
{
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        GetCard().RemoveFromGame(true);
    }
}
