using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAshLegionReaver : Trigger
{
    public Ability otherAbility;
    public bool canTrigger = true;

    public override void HeroDestroyed(Card sourceCard, Ability ability, int damageAmount, Hero hero)
    {
        if (hero == GetCard() && canTrigger)
        {
            if (GetOtherReaver().GetHP() > 0 && GetOtherReaver().GetZone() == MenuControl.Instance.battleMenu.board)
            {
                canTrigger = false;
                MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                {
                    if (GetOtherReaver().GetHP() > 0 && GetOtherReaver().GetZone() == MenuControl.Instance.battleMenu.board)
                    {
                        otherAbility.PerformAbility(GetCard(), hero.player.GetHero().GetTile());
                    }
                });
            }
        }
    }

    Hero GetOtherReaver()
    {
        foreach (Card card in GetCard().player.allCards)
        {
            if (card is Hero && card != GetCard())
            {
                return (Hero)card;
            }
        }
        return null;
    }
}
