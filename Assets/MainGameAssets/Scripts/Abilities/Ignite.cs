using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ignite : Ability
{

    public Effect burnEffectTemplate;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        foreach (Card card in MenuControl.Instance.battleMenu.GetAllUnitsOnBoard())
        {
            if (card.GetZone() == MenuControl.Instance.battleMenu.board)
            {
                Unit unit = (Unit)card;

                if (unit.GetEffectsWithTemplate(burnEffectTemplate).Count > 0)
                {
                    Effect burn = unit.GetEffectsWithTemplate(burnEffectTemplate)[0];
                    unit.SufferDamage(sourceCard, this, burn.remainingCharges);
                }
            }
        }
    }
}
