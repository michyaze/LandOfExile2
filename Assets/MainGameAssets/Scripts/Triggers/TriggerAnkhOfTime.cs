using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnkhOfTime : Trigger
{
    public override void HeroDestroyed(Card sourceCard, Ability ability, int damageAmount, Hero hero)
    {
        if (GetCard().GetZone() != MenuControl.Instance.battleMenu.removedFromGame && hero == GetCard().player.GetHero())
        {
            hero.ChangeCurrentHP(this, 10);
            GetCard().RemoveFromGame(true);
        }
    }
}
