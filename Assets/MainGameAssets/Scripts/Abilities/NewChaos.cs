using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewChaos : Ability
{
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        foreach (Minion minion in GetCard().player.GetMinionsOnBoard())
        {
            foreach (Trigger trigger in minion.GetComponentsInChildren<Trigger>())
            {
                trigger.MinionSummoned(minion);
            }
        }
    }
}
