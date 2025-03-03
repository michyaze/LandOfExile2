using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageModifierKoboldSneak : DamageModifier
{
    public int amountToAdd = 2;

    public override int ModifyAmount(Card sourceCard, Ability ability, Unit unit, int currentAmount)
    {
        Unit thisUnit = (Unit)GetCard();
        if (thisUnit.GetZone() == MenuControl.Instance.battleMenu.board && unit.player != GetCard().player)
        {
            if (thisUnit.GetAdjacentTiles().Contains(unit.GetTile()) && ability is Attack && ability.GetCard() is Minion)
            {
                return currentAmount + amountToAdd;
            }
        }

        return currentAmount;
    }
}
