using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterUnitsOfTemplateOnBoard: TriggerFilter
{
    public Card cardTemplate;
    public int minNumber = -1;
    public int maxNumber = -1;

    public override bool Check(Card sourceCard, Tile targetTile, int amount = 0)
    {
        int count = 0;
        foreach (Card card in MenuControl.Instance.battleMenu.GetAllUnitsOnBoard())
        {
            if (card.cardTemplate.UniqueID == cardTemplate.UniqueID)
            {
                count += 1;
            }
        }

        if (minNumber > -1 && count < minNumber) return false;
        if (maxNumber > -1 && count > maxNumber) return false;

        return true;
    }
}
