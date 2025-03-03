using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManthriSong : Ability
{
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        foreach (Minion minion in sourceCard.player.GetMinionsOnBoard().ToArray())
        {

            minion.ChangePower(this, minion.currentPower + 1);
            minion.ChangeCurrentHP(this, minion.currentHP + 1);

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

        foreach (Minion minion in sourceCard.player.GetOpponent().GetMinionsOnBoard().ToArray())
        {
            minion.ChangePower(this, minion.currentPower - 1);
            minion.ChangeCurrentHP(this, minion.currentHP - 1);
        }

    }
}
