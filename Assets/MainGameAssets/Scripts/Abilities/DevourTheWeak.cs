using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevourTheWeak : Ability
{
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        Minion lowestPowerMinion = null;

        foreach (Card card in MenuControl.Instance.battleMenu.GetAllUnitsOnBoard())
        {
            if (card is Minion)
            {
                Minion minion = (Minion)card;
                if (lowestPowerMinion == null || minion.GetPower() < lowestPowerMinion.GetPower())
                {
                    lowestPowerMinion = minion;
                }
            }
        }

        if (lowestPowerMinion != null)
        {
            int health = lowestPowerMinion.GetHP();
            lowestPowerMinion.SufferDamage(sourceCard, this, 0, true);

            sourceCard.player.GetHero().Heal(sourceCard, this, health);
        }
    }
}
