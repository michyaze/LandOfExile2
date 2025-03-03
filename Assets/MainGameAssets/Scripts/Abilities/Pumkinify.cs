using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumkinify : Ability
{
    public Card cardTemplateToSummon;


    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        Card oldCard = targetTile.GetUnit();
        oldCard.RemoveFromGame();

        Card newCard = sourceCard.player.CreateCardInGameFromTemplate(cardTemplateToSummon);
        newCard.TargetTile(targetTile, false);

        TriggerEnchantment enchantment = newCard.gameObject.AddComponent<TriggerEnchantment>();
        enchantment.cardsToReturn.Add(oldCard);


    }
}
