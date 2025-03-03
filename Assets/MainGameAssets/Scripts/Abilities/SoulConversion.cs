using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulConversion : Ability
{

    public int damageAmount;
    public int manaToGain;
    public int cardsToDraw;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        Unit unit = targetTile.GetUnit();
        unit.SufferDamage(GetCard(), this, damageAmount);
        if (unit.GetZone() != MenuControl.Instance.battleMenu.board)
        {
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                sourceCard.player.ChangeMana(manaToGain);
                for (int ii = 0; ii < cardsToDraw; ii += 1)
                {
                    sourceCard.player.DrawACard();
                }
            });
        }
    }
}
