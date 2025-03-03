using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyCanTargetManthriElite : AnyCanTarget
{
    public int minEnemyUnits = 4;

    public override bool CanTarget(Card sourceCard, Tile tile)
    {
        if (sourceCard.player != GetCard().player)
        {
            if (GetCard().player.GetOpponent().cardsOnBoard.Count < minEnemyUnits && sourceCard is Unit && tile == ((Unit)GetCard()).GetTile())
            {
                return false;
            }

        }

        return true;
    }
}
