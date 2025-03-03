using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MightOfTheClan : Ability
{
    public int healPerFriendlyMinion = 3;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        int totalHeal = healPerFriendlyMinion * GetCard().player.GetMinionsOnBoard().Count;
        if (totalHeal > 0)
        {
            GetCard().player.GetHero().Heal(GetCard(), this, totalHeal);
        }
    }
}
