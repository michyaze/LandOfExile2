using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectTheEggs : Ability
{
    public Card eggTemplate;
    public int powerIncrease = 0;
    public int hpIncrease = 2;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        int eggCount = 0;
        foreach (Card card in sourceCard.player.cardsOnBoard)
        {
            if (card.cardTemplate.UniqueID == eggTemplate.UniqueID)
            {
                eggCount += 1;
            }
        }

        if (eggCount > 0)
        {
            foreach (Minion minion in sourceCard.player.GetMinionsOnBoard())
            {
                if (minion.cardTemplate.UniqueID != eggTemplate.UniqueID)
                {
                    if (powerIncrease > 0)
                        minion.ChangePower(this, minion.currentPower + (powerIncrease * eggCount));

                    if (hpIncrease > 0)
                    {
                        minion.ChangeMaxHP(this, minion.GetInitialHP() + (hpIncrease * eggCount));
                        minion.ChangeCurrentHP(this, minion.currentHP + (hpIncrease * eggCount));
                    }
                }
            }
        }
    }
}
