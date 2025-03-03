using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FromTheVoid : Ability
{

    public Ability summoningAbility;
    public Ability changePowerHPAbility;
    public Minion minionTemplate;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        foreach (Minion minion in sourceCard.player.GetMinionsOnBoard())
        {
            if (minion.cardTemplate.UniqueID == minionTemplate.UniqueID)
            {
                changePowerHPAbility.PerformAbility(sourceCard, minion.GetTile(), amount);
                return;
            }
        }

        summoningAbility.PerformAbility(sourceCard, targetTile, amount);
    }
}
