using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enchant : Ability
{
    public Effect templateEffect;
    public int charges;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        Effect effect = targetTile.GetUnit().ApplyEffect(sourceCard, this, templateEffect, charges);
        if (effect == null)
        {
            return;
        }
        effect.GetComponent<TriggerEnchantment>().cardsToReturn.Add(GetCard());
        GetCard().ExhaustThisCard();
    }

}
