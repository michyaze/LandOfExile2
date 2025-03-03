using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOnDefenderWillBeDestroyedTargetAttacker : Trigger
{
    public int abilitiesToCallBelow = 1;

    public TriggerFilter triggerFilter;

    public void CallAbilitiesBelow(Card sourceCard, Unit attacker, int amount = 0)
    {
        if (triggerFilter != null && !triggerFilter.Check(sourceCard, attacker.GetTile(), amount)) return;

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

                        ability.PerformAbility(sourceCard, attacker.GetTile(), amount);

                        if (countDown == 0) break;
                    }
                }

            }

        });
    }


    public override void UnitDamaged(Card sourceCard, Unit unit, Ability ability, int damageAmount,
        bool triggerUnitDamage = true)
    {
        if (!(GetCard() is Unit) || GetCard().GetZone() != MenuControl.Instance.battleMenu.board) return;

        var attacker = ability.GetCard() as Unit;
        if (attacker == null)
        {
            attacker = ability.GetCard().player.GetHero();
        }
        if (unit == GetCard() && unit.GetHP() <= 0 && attacker is Unit)
        {

            CallAbilitiesBelow(GetCard(), attacker, damageAmount);

        }

    }
}
