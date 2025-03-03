using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum CardType
{ 
    Minion=1,
  Spell=2,
  Weapon=2<<1,
  Item = 2<<2,
  Treasure = 2<<3,
}
public class PerformToHandCards : Ability
{
    
    public  CardType cardType;
    public Ability anotherAbility;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        foreach (var card in GetCard().player.cardsInHand)
        {
            if (card.isAnyTypeFollows(cardType))
            {
                anotherAbility.PerformAbility(card, targetTile,amount);
            }
        }
    }
}
