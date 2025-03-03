using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnThisCardToHand : Ability
{
    public bool returnSourceCard = false;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (returnSourceCard)
        {
             
            sourceCard.PutIntoZone(MenuControl.Instance.battleMenu.hand);
        }
        else
        {
            GetCard().PutIntoZone(MenuControl.Instance.battleMenu.hand);
        }
    }
}
