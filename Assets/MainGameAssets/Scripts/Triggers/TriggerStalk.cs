using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStalk : Trigger
{
    public Unit unitStalked;
    public bool willTrigger;
    public Ability otherAbilityToPerform; 

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        unitStalked = targetTile.GetUnit();
    }

    public override void MinionDestroyed(Card sourceCard, Ability ability, int damageAmount, Minion minion)
    {
        if (minion == unitStalked)
        {
            willTrigger = true;
            unitStalked = null;
        }
    }

    public override void HeroDestroyed(Card sourceCard, Ability ability, int damageAmount, Hero hero)
    {
        if (hero == unitStalked)
        {
            willTrigger = true;
            unitStalked = null;
        }
    }

    public override void TurnStarted(Player player)
    {
        if (player == GetCard().player)
        {
            if (willTrigger)
            {
                willTrigger = false;
                MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                {
                    otherAbilityToPerform.PerformAbility(GetCard(), GetCard().player.GetHero().GetTile());
                });
            }
        }
    }
}
