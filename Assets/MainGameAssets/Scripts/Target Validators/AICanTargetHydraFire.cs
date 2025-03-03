using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICanTargetHydraFire : AICanTarget
{
    public Card headTemplate;

    public override bool CanTarget(Tile tile)
    {
        foreach (Tile tile2 in tile.GetAdjacentTilesLinear(1))
        {
            if (tile2.GetUnit() != null && tile2.GetUnit().cardTemplate.UniqueID == headTemplate.UniqueID)
            {
                return true;
            }
        }

        return false;
    }
}
