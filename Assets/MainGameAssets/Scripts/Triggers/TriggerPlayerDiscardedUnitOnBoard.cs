using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// is this used?
public class TriggerPlayerDiscardedUnitOnBoard : Trigger
{

    public int abilitiesToCallBelow = 1;
    public TriggerFilter triggerFilter;
    public int triggerEveryXTimes = 1;
    public int currentTriggers;
    public bool immediatelyPerform;
    public bool opponentDiscarded;

    public void CallAbilitiesBelow(Card sourceCard, Unit targetUnit, int amount = 0, Tile targetTile = null)
    {
        currentTriggers += 1;
        if (currentTriggers < triggerEveryXTimes) return;
        currentTriggers = 0;

        if (triggerFilter != null && !triggerFilter.Check(sourceCard, targetUnit.GetTile(), amount)) return;

        System.Action actionToPerform = () =>
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

                        ability.PerformAbility(sourceCard, targetTile == null ? targetUnit.GetTile() : targetTile, amount);

                        if (countDown == 0) break;
                    }
                }

            }

        };

        if (immediatelyPerform)
        {
            actionToPerform();
        }
        else
        {
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), actionToPerform);
        }
    }

   

    public override void CardDiscarded(Card card, bool automaticDiscard)
    {

        if (!(GetCard() is NewWeapon) && (!(GetCard() is Unit) || GetCard().GetZone() != MenuControl.Instance.battleMenu.board)) return;

        if ((opponentDiscarded && card.player != GetCard().player) || (!opponentDiscarded && card.player == GetCard().player))
        {
            CallAbilitiesBelow(card, (Unit)GetCard());
        }
    }

}
