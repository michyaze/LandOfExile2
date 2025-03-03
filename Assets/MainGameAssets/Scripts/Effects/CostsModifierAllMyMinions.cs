using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostsModifierAllMyMinions : CostsModifier
{
    public int amountToChange;
    public bool andSpells;

    public override int ModifyAmount(Card card, int currentAmount)
    {
        if (GetCard().GetZone() == MenuControl.Instance.battleMenu.board)
        {
            if (card is Minion && card.player == GetCard().player)
            {
                return currentAmount + amountToChange;
            }
            if (andSpells && card.cardTags.Contains(MenuControl.Instance.spellTag) && card.player == GetCard().player)
            {
                return currentAmount + amountToChange;
            }
        }

        return currentAmount;
    }
}
