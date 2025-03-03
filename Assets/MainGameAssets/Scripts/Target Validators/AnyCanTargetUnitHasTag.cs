using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyCanTargetUnitHasTag : AnyCanTarget
{
    public List<CardTag> tagsOwned = new List<CardTag>();
    public List<CardTag> tagsNotOwned = new List<CardTag>();

    public override bool CanTarget(Card sourceCard, Tile tile)
    {
        if (sourceCard != GetCard()) return true;

        if (tile.GetUnit() != null)
        {
            foreach (CardTag cardTag in tile.GetUnit().cardTags)
            {
                if (tagsNotOwned.Contains(cardTag)) return false;

                if (tagsOwned.Contains(cardTag)) return true;
            }

        }

        if (tagsNotOwned.Count > 0) return true;
        return false;
    }
}
