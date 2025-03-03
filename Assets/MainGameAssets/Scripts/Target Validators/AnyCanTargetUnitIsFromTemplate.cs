using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyCanTargetUnitIsFromTemplate : AnyCanTarget
{
    public Card templateCard;

    public override bool CanTarget(Card sourceCard, Tile tile)
    {
        if (sourceCard != GetCard()) return true;

        return (tile.GetUnit() != null && tile.GetUnit().cardTemplate.UniqueID == templateCard.UniqueID);
    }
}
