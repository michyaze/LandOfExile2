using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntoThePot : Ability
{
    public Card thePotTemplate;
    public Effect cookingEffectTemplate;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        Minion thePot = null;
        foreach (Minion minion in sourceCard.player.GetMinionsOnBoard())
        {
            if (minion.cardTemplate.UniqueID == thePotTemplate.UniqueID)
            {
                thePot = minion;
            }
        }

        if (thePot != null)
        {

            var hero = sourceCard.player.GetHero();

            List<Minion> minionsInRange = new List<Minion>();
            foreach (Minion minion in sourceCard.player.GetOpponent().GetMinionsOnBoard())
            {
                foreach (Tile tile in hero.GetTiles())
                {
                    if (minion.GetTile().GetAdjacentTilesLinear(2).Contains(tile))
                    {
                        minionsInRange.Add(minion);
                    }
                }
            }

            if (minionsInRange.Count > 0)
            {
                minionsInRange[Random.Range(0, minionsInRange.Count)].SufferDamage(sourceCard, this, 0, true);

                thePot.ApplyEffect(sourceCard, this, cookingEffectTemplate, 1);
            }

        }

    }
}
