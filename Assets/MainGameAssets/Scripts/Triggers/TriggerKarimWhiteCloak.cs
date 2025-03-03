using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerKarimWhiteCloak : Trigger
{
    public override void MinionDestroyed(Card sourceCard, Ability ability, int damageAmount, Minion minion)
    {
        if (minion == GetCard())
        {
            foreach (Tile tile in ((Unit)GetCard()).GetAdjacentTiles(1))
            {
                if (tile.GetUnit() is Minion)
                {
                    foreach(Trigger trigger in tile.GetUnit().GetComponentsInChildren<Trigger>())
                    {
                        try
                        {
                            trigger.MinionDestroyed(GetCard(), this, 0, (Minion)tile.GetUnit());
                        }
                        catch (System.Exception e)
                        {
                            Debug.LogError(e);
                        }

                    }
                }
            }
        }
    }
}
