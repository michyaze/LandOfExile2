using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageModifierFriendlyMinionsCannotDie : DamageModifier
{
    public override int ModifyAmount(Card sourceCard, Ability ability, Unit unit, int currentAmount)
    {
        if (unit is Minion && unit.GetZone() == MenuControl.Instance.battleMenu.board && unit.player == GetCard().player)
        {
            if (currentAmount >= unit.GetHP())
            {
                return Mathf.Max(0, unit.GetHP() - 1);
            }
        }

        return currentAmount;
    }
}
