using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exhaust : Ability
{
    public override float PerformAnimationTime(Card sourceCard)
    {
        return MenuControl.Instance.battleMenu.GetPlaySpeed()  * 2;
    }

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        GetCard()?.ExhaustThisCard();
    }
}
