using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningSoul : Ability
{

    public Effect burnEffectTemplate;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        List<Minion> handMinions = new List<Minion>();
        foreach (Card card in sourceCard.player.cardsInHand)
        {
            if (card is Minion)
            {
                handMinions.Add((Minion)card);
            }
        }

        if (handMinions.Count > 0)
        {
            Minion randomMinion = handMinions[Random.Range(0, handMinions.Count)];
            int power = randomMinion.GetPower();

            randomMinion.ExhaustThisCard();

            targetTile.GetUnit().ApplyEffect(sourceCard, this, burnEffectTemplate, power);
        }
    }
}
