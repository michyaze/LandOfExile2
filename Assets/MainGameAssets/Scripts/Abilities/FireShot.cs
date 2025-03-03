using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShot : Ability
{

    public int damageAmount = 5;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (GetEffect().remainingCharges == 1)
        {
            GetCard().player.GetOpponent().GetHero().SufferDamage(GetCard(), this, damageAmount);
            if ( GetCard().GetZone() == MenuControl.Instance.battleMenu.board)
            {
                ((Unit)GetCard()).SufferDamage(GetCard(), this, 0, true);
            }
        }
    }
}
