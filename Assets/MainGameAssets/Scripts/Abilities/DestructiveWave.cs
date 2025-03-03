using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructiveWave : Ability
{
    public int damageAmount = 2;
    public bool targetAll = false;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        if (DealDamage(false))
        {
            DealDamage(true);
        }

    }

    bool DealDamage(bool delayed)
    {
        List<Minion> allMinions= GetCard().player.GetOpponent().GetMinionsOnBoard();
        if (targetAll)
        {
            allMinions.AddRange(GetCard().player.GetMinionsOnBoard());
        }
        
        
        if (delayed)
        {
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                bool minionDied = false;
                foreach (Minion minion in allMinions)
                {
                    minion.SufferDamage(GetCard(), this, damageAmount);
                    if (minion.GetZone() != MenuControl.Instance.battleMenu.board) minionDied = true;
                }
                if (minionDied)
                {
                    DealDamage(true);
                }

            });
            return false;
        }

        bool minionDestroyed = false;
        foreach (Minion minion in allMinions)
        {
            if (minion.GetZone() == MenuControl.Instance.battleMenu.board)
            {
                minion.SufferDamage(GetCard(), this, damageAmount);
                if (minion.GetZone() != MenuControl.Instance.battleMenu.board) minionDestroyed = true;
            }
        }

        return minionDestroyed;
    }
}
