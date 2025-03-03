using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandersCannon : Ability
{
    public int damageAmount = 1; 
    public Effect burnEffectTemplate;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        int count = 1;

        if (((Unit)GetCard()).GetEffectsWithTemplate(burnEffectTemplate).Count > 0)
        {
            count += ((Unit)GetCard()).GetEffectsWithTemplate(burnEffectTemplate)[0].remainingCharges;
        }

        for (int ii = 0; ii < count; ii += 1)
        {
            List<Minion> minions = sourceCard.player.GetMinionsOnBoard();
            minions.AddRange(sourceCard.player.GetOpponent().GetMinionsOnBoard());

            if (minions.Count > 0)
            {
                minions.Shuffle();
                minions[0].SufferDamage(sourceCard, this, damageAmount);
            }
        }
    }
}
