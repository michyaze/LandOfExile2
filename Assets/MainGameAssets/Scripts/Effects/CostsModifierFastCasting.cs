using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostsModifierFastCasting : CostsModifier
{
    public int amountToChange = -1;
    public bool firstTriggerInstance = true;
    public bool onlyOnSpell;

    public bool canModifyCard(Card card)
    {
        if (onlyOnSpell)
        {
            return card.isSpell();
        }

        return true;
    }
    public override int ModifyAmount(Card card, int currentAmount)
    {
        if (firstTriggerInstance && GetCard().GetZone() == MenuControl.Instance.battleMenu.board && canModifyCard(card))
        {
            if (card.initialCost > 0 && card.player == GetCard().player)
            {
                return currentAmount + amountToChange;
            }
        }

        return currentAmount;
    }
}
