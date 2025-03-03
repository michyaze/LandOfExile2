using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCards : Ability
{
    public bool playerDraws = true;
    public bool enemyDraws;
    public int cardsToDraw = 1;
    public bool useChargesInstead;
    public bool useChargesAsWell;
    public bool noShuffle;


    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        int finalNumber = cardsToDraw == 0 ? amount : cardsToDraw;
        if (useChargesInstead) finalNumber = GetEffect().remainingCharges;
        if (useChargesAsWell) finalNumber += GetEffect().remainingCharges;

        for (int ii = 0; ii < finalNumber; ii += 1)
        {
            if (noShuffle && sourceCard.player.cardsInDeck.Count == 0) return; 

            if (playerDraws) sourceCard.player.DrawACard();
            if (enemyDraws) sourceCard.player.GetOpponent().DrawACard();
        }
    }
}
