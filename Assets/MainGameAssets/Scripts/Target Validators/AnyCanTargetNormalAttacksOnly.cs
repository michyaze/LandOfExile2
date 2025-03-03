using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyCanTargetNormalAttacksOnly : AnyCanTarget
{


    public override bool CanTarget(Card sourceCard, Tile tile)
    {
        if (sourceCard == GetCard())
        {
            if (tile.GetUnit() != null && tile.GetUnit().activatedAbility != null && tile.GetUnit().activatedAbility.GetTargetValidator() != null && tile.GetUnit().activatedAbility.GetTargetValidator() is TargetLinear)
            {
                if (((TargetLinear)tile.GetUnit().activatedAbility.GetTargetValidator()).allUnits)
                {
                    return false;
                }
                if (((TargetLinear)tile.GetUnit().activatedAbility.GetTargetValidator()).myTeam)
                {
                    return false;
                }
                if (((TargetLinear)tile.GetUnit().activatedAbility.GetTargetValidator()).notMyHero)
                {
                    return false;
                }
                if (((TargetLinear)tile.GetUnit().activatedAbility.GetTargetValidator()).notAdjacent)
                {
                    return false;
                }
                if (((TargetLinear)tile.GetUnit().activatedAbility.GetTargetValidator()).allowDiagonal)
                {
                    return false;
                }
            }
        }

        return true;
    }
}
