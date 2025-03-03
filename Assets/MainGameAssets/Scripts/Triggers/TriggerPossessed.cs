using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPossessed : Trigger
{
    public Player originalPlayer;
    public Ability changePlayer;

    public override void UnitAppliedEffect(Unit unit, Ability ability, Effect effect, int charges)
    {
        if (effect == GetEffect())
        {
            if (originalPlayer == null) originalPlayer = GetCard().player;
        }
    }

    public override void TurnEnded(Player player)
    {
        if (player == GetCard().player)
        {
            if (GetEffect().remainingCharges == 1)
            {
                if (GetCard().player != originalPlayer)
                {
                    changePlayer.PerformAbility(GetCard(), ((Unit)GetCard()).GetTile());
                }
            }

            GetEffect().ConsumeCharges(this,1);

        }
    }
}
