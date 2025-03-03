using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillChangeMovementType : Ability
{
    public TargetValidator movementType;
    public TargetValidator overrideMovementType;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (sourceCard != null && sourceCard is Unit unit)
        {
            // Stack Leap Skills
            if (MenuControl.Instance.heroMenu.hero.movementType.UniqueID.Contains("Leap") &&
                MenuControl.Instance.heroMenu.hero.movementType.UniqueID != movementType.UniqueID &&
                overrideMovementType != null)
            {
                unit.movementType = overrideMovementType;
            }
            else
            {
                unit.movementType = movementType;
            }
        }
        else
        {
            // Stack Leap Skills
            if (MenuControl.Instance.heroMenu.hero.movementType.UniqueID.Contains("Leap") &&
                MenuControl.Instance.heroMenu.hero.movementType.UniqueID != movementType.UniqueID &&
                overrideMovementType != null)
            {
                MenuControl.Instance.heroMenu.hero.movementType = overrideMovementType;
            }
            else
            {
                MenuControl.Instance.heroMenu.hero.movementType = movementType;
            }
        }

    }
}
