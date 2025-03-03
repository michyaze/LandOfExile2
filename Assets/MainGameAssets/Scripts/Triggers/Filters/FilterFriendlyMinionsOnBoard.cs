using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterFriendlyMinionsOnBoard: TriggerFilter
{

    public int minNumber = -1;
    public int maxNumber = -1;

    public override bool Check(Card sourceCard, Tile targetTile, int amount = 0)
    {
        int count = GetCard().player.GetMinionsOnBoard().Count;

        if (minNumber > -1 && count < minNumber) return false;
        if (maxNumber > -1 && count > maxNumber) return false;

        return true;
    }
}
