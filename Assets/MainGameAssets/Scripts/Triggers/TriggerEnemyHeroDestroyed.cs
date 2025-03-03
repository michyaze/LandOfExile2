using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnemyHeroDestroyed : Trigger
{
    public Ability otherAbilityToPerform;
    public bool immediately;
    public bool byThisUnit;

    public override void HeroDestroyed(Card sourceCard, Ability ability, int damageAmount, Hero hero)
    {
        if (byThisUnit && ability.GetCard() != GetCard()) return;

        if (hero.player != GetCard().player)
        {
            if (immediately)
            {
                otherAbilityToPerform.PerformAbility(sourceCard, GetCard().player.GetHero().GetTile());
            }
            else
            {
                MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                {
                    otherAbilityToPerform.PerformAbility(sourceCard, GetCard().player.GetHero().GetTile());
                });
            }
        }
    }
}
