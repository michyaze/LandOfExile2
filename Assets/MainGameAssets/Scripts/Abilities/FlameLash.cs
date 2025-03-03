using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameLash : Ability
{
    public Effect effectTemplate;
    public int initialAmount = 4;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        int damageAmount = initialAmount - sourceCard.player.GetOpponent().cardsOnBoard.Count;

        //foreach (Card card in sourceCard.player.GetOpponent().cardsOnBoard.ToArray())
        //{
        //    ((Unit)card).ApplyEffect(sourceCard, this, effectTemplate, 2); //Burn
        //}

        if (damageAmount > 0)
        {
            foreach (Card card in sourceCard.player.GetOpponent().cardsOnBoard.ToArray())
            {
                ((Unit)card).SufferDamage(sourceCard, this, damageAmount);
            }
        }


    }
}
