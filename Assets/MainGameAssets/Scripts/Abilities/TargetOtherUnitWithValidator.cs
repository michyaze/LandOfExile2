using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetOtherUnitWithValidator : Ability
{
    public Ability otherAbilityToPerform;
    public int randomSelectCount = 1;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        var sourceUnit = sourceCard as Unit;
        if (sourceUnit == null)
        {
            sourceUnit = sourceCard.player.GetHero();
        }

        List<Unit> candidateUnits = new List<Unit>();
        foreach (Unit unit in MenuControl.Instance.battleMenu.GetAllUnitsOnBoard())
        {
            if (GetTargetValidator().CanUnitTargetTile(sourceUnit, unit.GetTile()))
            {
                candidateUnits.Add(unit);
            }
        }

        if (randomSelectCount <0)
        {
            foreach (var unit in candidateUnits)
            {
                
                otherAbilityToPerform.PerformAbility(sourceCard,unit.GetTile());
            }
        }
        else
        {
            
            for (int i = 0; i < Math.Min(randomSelectCount, candidateUnits.Count); i++)
            {
                var unit = candidateUnits.PickItem();
                otherAbilityToPerform.PerformAbility(sourceCard,unit.GetTile());
            }
        }
    }
}
