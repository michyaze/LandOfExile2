using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRelentless : Trigger
{
    public override void HeroDestroyed(Card sourceCard, Ability ability, int damageAmount, Hero hero)
    {
        if (GetCard().GetZone() != MenuControl.Instance.battleMenu.removedFromGame && hero == GetCard().player.GetHero())
        {
            hero.ChangeCurrentHP(this, 1);
            GetEffect().ConsumeCharges(this);
        }
    }
}
