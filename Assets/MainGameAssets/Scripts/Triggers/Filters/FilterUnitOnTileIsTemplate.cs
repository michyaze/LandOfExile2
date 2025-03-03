using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterUnitOnTileIsTemplate : TriggerFilter
{
    public Card cardTemplate;
    public bool isNotTemplate;

    public override bool Check(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (targetTile.GetUnit() != null)
        {
            if (isNotTemplate)
            {
                if (targetTile.GetUnit().cardTemplate.UniqueID != cardTemplate.UniqueID)
                {
                    return true;
                }
            }
            else
            {
                if (targetTile.GetUnit().cardTemplate.UniqueID == cardTemplate.UniqueID)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
