using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICanTargetEnemiesOnly : AICanTarget
{


    public override bool CanTarget(Tile tile)
    {
        if (tile.GetUnit() == null || tile.GetUnit().player == GetCard().player) return false;

        return true;
    }
}
