using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnowDeath : Ability
{
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (targetTile.GetUnit() != null && targetTile.GetUnit() is Minion)
        {
            foreach (Trigger trigger in targetTile.GetUnit().GetComponentsInChildren<Trigger>())
            {
                try
                {
                    trigger.MinionDestroyed(sourceCard, this, 0, (Minion)targetTile.GetUnit());
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                }

            }
        }
    }
}
