using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerUnitEffectsOthersWithTag : Trigger
{
    public int abilitiesToCallBelow = 1;
    public TriggerFilter triggerFilter;
    public CardTag cardTag;
    public bool enemyUnits;
    public bool friendlyUnits;

    public void CallAbilitiesBelow(Card sourceCard, Unit targetUnit, int amount = 0)
    {
        if (triggerFilter != null && !triggerFilter.Check(sourceCard, targetUnit.GetTile(), amount)) return;


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

                        ability.PerformAbility(sourceCard, targetUnit.GetTile(), amount);

                        if (countDown == 0) break;
                    }
                }

            }

        });
    }


    public override void MinionSummoned(Minion minion)
    {
        if (abilitiesToCallBelow > 0)
        {
            if (minion == GetCard())
            {
                foreach (Unit unit in MenuControl.Instance.battleMenu.GetAllUnitsOnBoard())
                {

                    if ((enemyUnits && unit.player == GetCard().player.GetOpponent()) || (friendlyUnits && unit.player == GetCard().player))
                    {
                        if (cardTag == null || unit.cardTags.Contains(cardTag))
                        {

                            CallAbilitiesBelow(GetCard(), unit);

                        }
                    }
                }

            }

            else
            {
                if (!(GetCard() is Unit) || GetCard().GetZone() != MenuControl.Instance.battleMenu.board) return;

                if ((enemyUnits && minion.player == GetCard().player.GetOpponent()) || (friendlyUnits && minion.player == GetCard().player))
                {
                    if (cardTag == null || minion.cardTags.Contains(cardTag))
                    {
                        CallAbilitiesBelow(GetCard(), minion);
                    }
                }
            }
        }
    }
}
