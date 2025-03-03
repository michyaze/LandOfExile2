using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICanTargetAscensionLevelTurnRestriction : AICanTarget
{
    public int ascensionLevel;
    public int turn;

    public override bool CanTarget(Tile tile)
    {
        if (MenuControl.Instance.battleMenu.currentRound <= turn && MenuControl.Instance.heroMenu.ascensionMode >= ascensionLevel) return false;

        return true;
    }
}
