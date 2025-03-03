using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiphonStrength : Ability
{
    public Effect growStrengthEffectTemplate;
    public Effect sapStrengthEffectTemplate;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        List<Minion> minions = sourceCard.player.GetMinionsOnBoard();
        int growthAmount = minions.Count;
        foreach (Minion minion in minions)
        {
            minion.ApplyEffect(sourceCard, this, sapStrengthEffectTemplate, 0);
        }

        for (int ii = 0; ii < growthAmount; ii += 1)
        {
            sourceCard.player.GetHero().ApplyEffect(sourceCard, this, growStrengthEffectTemplate, 0);
        }

    }
}
