using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLastFriendlyMinionDestroyed : Trigger
{
    public Ability otherAbilityToPerform;
    public bool wasThisCard;


    public override void MinionDestroyed(Card sourceCard, Ability ability, int damageAmount, Minion minion)
    {
        if (minion.player == GetCard().player)
        {
            if (GetCard().player.GetMinionsOnBoard().Count == 1 && GetCard().player.GetMinionsOnBoard()[0] == minion) //still includes the destroyed minion
            {
                if (!wasThisCard || GetCard() == minion)
                {
                    otherAbilityToPerform.PerformAbility(sourceCard, GetCard().player.GetHero().GetTile());
                }
            }
        }
    }
}
