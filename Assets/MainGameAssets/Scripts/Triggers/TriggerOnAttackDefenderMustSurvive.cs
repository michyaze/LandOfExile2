using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOnAttackDefenderMustSurvive : Trigger
{
    public bool eventMustTriggerOnThisCard = true;
    public int abilitiesToCallBelow = 1;
    public TriggerFilter triggerFilter;

    public void CallAbilitiesBelow(Card sourceCard, Unit defender, int amount = 0)
    {
        if (triggerFilter != null && !triggerFilter.Check(sourceCard, defender.GetTile(), amount)) return;

        MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
        {
            if (defender.GetZone() != MenuControl.Instance.battleMenu.board) return;

            bool startCounting = false;
            int countDown = abilitiesToCallBelow;
            if (this == null)
            {
                return;
            }
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
                        //if (ability.targetValidator ==null || ability.CanTargetTile(sourceCard, defender.GetTile()))
                        {
                            ability.PerformAbility(sourceCard, defender.GetTile(), amount);
                        }

                        if (countDown == 0) break;
                    }
                }

            }

        });
    }

    public override void UnitAttacks(Unit attacker, Unit defender, Tile targetTile, bool initialAttack)
    {

        if (!(GetCard() is NewWeapon) && (!(GetCard() is Unit) || GetCard().GetZone() != MenuControl.Instance.battleMenu.board)) return;

        if (eventMustTriggerOnThisCard)
        {
            if (attacker == GetCard() && defender.GetZone() == MenuControl.Instance.battleMenu.board)
            {
                CallAbilitiesBelow(attacker, defender);
            }

        }
        else
        {
            if (abilitiesToCallBelow > 0 && defender.GetZone() == MenuControl.Instance.battleMenu.board)
            {
                CallAbilitiesBelow(attacker, defender);
            }
        }
    }
}
