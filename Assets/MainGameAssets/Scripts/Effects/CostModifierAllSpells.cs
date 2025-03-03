using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostModifierAllSpells: CostsModifier
{
    public int amountToChange;
    public bool andSpells;

    public override int ModifyAmount(Card card, int currentAmount)
    {
        if (GetCard().GetZone() == MenuControl.Instance.battleMenu.board)
        {
            if (card.isSpell() && card.player == GetCard().player)
            {
                return currentAmount + amountToChange;
            }
        }

        return currentAmount;
    }
}