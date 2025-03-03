using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageModifierAllUnitsMultiplied : DamageModifier
{
    public float multiplier = 2;
    public bool notThisUnit;
    public bool unitsMustBeMissingHealth;

    public override int ModifyAmount(Card sourceCard, Ability ability, Unit unit, int currentAmount)
    {
        if (((Unit)GetCard()).GetZone() == MenuControl.Instance.battleMenu.board)
        {
            if (notThisUnit && unit == GetCard()) return currentAmount;

            if (unitsMustBeMissingHealth && unit.GetHP() == unit.GetInitialHP()) return currentAmount;

            return Mathf.RoundToInt(currentAmount * multiplier);

        }

        return currentAmount;
    }


}
