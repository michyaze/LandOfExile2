using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Ability
{
    public int amountToHeal;
    public bool useChargesAsDamageAmount;
    public bool useAmountToHeal;
    public bool healAll;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        var value = amount > 0 ? amount : amountToHeal;
        value = useAmountToHeal? amountToHeal:value;
        if (useChargesAsDamageAmount)
        {
            value = GetEffect().remainingCharges;
        }

        var unit = targetTile.GetUnit();
        if (unit == null)
        {
            return;
        }
        if (healAll)
        {
            value = unit.initialHP - unit.currentHP;
        }
        targetTile.GetUnit().Heal(sourceCard, this, value);

    }
}
