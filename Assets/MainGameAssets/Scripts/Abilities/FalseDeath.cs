using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalseDeath : Ability
{
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        foreach (Minion minion in sourceCard.player.GetMinionsOnBoard().ToArray())
        {
            foreach (Trigger trigger in minion.GetComponentsInChildren<Trigger>())
            {
                try
                {
                    trigger.MinionDestroyed(sourceCard, this, 0, minion);
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                }

            }
        }
    }
}
