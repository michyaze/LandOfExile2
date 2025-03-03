using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayer : Ability
{
    public bool onlyToThisCardsPlayer;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (targetTile == null) return;
        Unit unit = targetTile.GetUnit();
        if (unit == null) return; 
        if (onlyToThisCardsPlayer && unit.player == GetCard().player) return;

        unit.player.cardsOnBoard.Remove(unit);
        unit.player = unit.player.GetOpponent();
        unit.player.cardsOnBoard.Add(unit);
    }
}

