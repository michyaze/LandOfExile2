using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaledDefense : Ability
{
    public List<Card> targetTemplates = new List<Card>();
    public Effect blockEffectTemplate;
    public int charges = 2;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        foreach (Minion minion in sourceCard.player.GetMinionsOnBoard())
        {
            bool cardInTargetTemplates = false;
            foreach (Card card2 in targetTemplates)
            {
                if (card2.UniqueID == minion.cardTemplate.UniqueID)
                {
                    cardInTargetTemplates = true;
                }
            }

            if (cardInTargetTemplates)
            {
                minion.ApplyEffect(sourceCard, this, blockEffectTemplate, charges);
            }
        }
    }
}
