using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOnAttackDefenderDestroyed : Trigger
{
    public int abilitiesToCallBelow = 1;
    public bool targetAttacker;
    public bool targetDefender;

    public void CallAbilitiesBelow(Card sourceCard, Unit defender, Tile targetTile)
    {
        MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
        {
            bool startCounting = false;
            int countDown = abilitiesToCallBelow;
            foreach (Ability ability in GetComponents<Ability>())
            {
                if (ability == this)
                {
                    startCounting = true;
                }
                else
                {
                    if (startCounting)
                    {
                        countDown -= 1;

                        ability.PerformAbility(sourceCard, targetTile == null ? defender.GetTile(): targetTile, 0);

                        if (countDown == 0) break;
                    }
                }

            }

        });
    }
    public override void HeroDestroyed(Card sourceCard, Ability ability, int damageAmount, Hero hero)
    {

        UnitDestroyed(sourceCard, ability, damageAmount, hero);
    }
    public override void MinionDestroyed(Card sourceCard, Ability ability, int damageAmount, Minion minion)
    {
        UnitDestroyed(sourceCard, ability, damageAmount, minion);
    }
    
    public void UnitDestroyed(Card sourceCard, Ability ability, int damageAmount, Unit minion)
    {
        //if (!(GetCard() is NewWeapon) && (!(GetCard() is Unit) || GetCard().GetZone() != MenuControl.Instance.battleMenu.board)) return;
        Unit attacker = null;
        if (GetCard() is Unit)
        {
            attacker = (Unit)GetCard();
        }else
        {
            attacker = GetCard().player.GetHero();
        }
        if (ability.GetCard() == GetCard())
        {
            if (targetAttacker)
            {
                CallAbilitiesBelow(GetCard(), attacker, null);
            }
            if (targetDefender)
            {
                CallAbilitiesBelow(GetCard(), minion, minion.GetTile());
            }
        }
    }
}
