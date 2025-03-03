using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifact : Castable
{
    public int initialCoolDown;
    public int currentCoolDown;
    public int unlockCost;

    public override void TargetTile(Tile tile, bool payCost)
    {
        currentCoolDown = initialCoolDown;
        base.TargetTile(tile, payCost);
    }

    public override bool CanTarget(Tile tile)
    {
        if (currentCoolDown > 0) return false;
        return base.CanTarget(tile);
    }

    // public override string GetDescription()
    // {
    //     return /*goldWorth>0?(MenuControl.Instance.GetLocalizedString("Flarestones") + ": " + 3):"" + */"\n\n" + base.GetDescription() + "\n\n" + ;
    // }

}
