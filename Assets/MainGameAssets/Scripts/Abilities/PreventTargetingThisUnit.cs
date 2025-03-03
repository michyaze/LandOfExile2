using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventTargetingThisUnit : PreventTargeting
{
    public bool enemyOnly;
    public override bool CanTarget(Card soureCard, Tile targetTile)
    {

        if (targetTile.GetUnit() == GetCard() && (!enemyOnly || soureCard.player != GetCard().player))
        {
            return false;
        }

        return true;
    }
}
