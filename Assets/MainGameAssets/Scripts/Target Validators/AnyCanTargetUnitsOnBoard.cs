using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyCanTargetUnitsOnBoard : AnyCanTarget
{
    public int minCount;
    public int maxCount;
    public bool dontCountLargeHeros;
    public bool friendlyUnitsOnly;

    public override bool CanTarget(Card sourceCard, Tile tile)
    {
        if (sourceCard != GetCard()) return true;

        int count = 0;
        foreach (Card card in MenuControl.Instance.battleMenu.GetAllUnitsOnBoard())
        {
            if (!dontCountLargeHeros ||!(card is LargeHero))
            {
                if (!friendlyUnitsOnly || card.player == sourceCard.player)
                {
                    count += 1;
                }
            }
        }

        if (minCount > 0 && count < minCount) return false;
        if (maxCount > 0 && count > maxCount) return false;


        return true;
    }
}
