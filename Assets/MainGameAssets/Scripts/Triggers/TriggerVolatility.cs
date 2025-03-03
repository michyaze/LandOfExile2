using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVolatility : Trigger
{
    public override void UnitDamaged(Card sourceCard, Unit unit, Ability ability, int damageAmount,
        bool triggerUnitDamage = true)
    {
        if (GetCard() == unit)
        {
            ((Unit)GetCard()).ApplyEffect(GetCard(), this, GetEffect().originalTemplate, damageAmount);
        }
    }

    public override void MinionDestroyed(Card sourceCard, Ability ability, int damageAmount, Minion minion)
    {
        if (minion == GetCard())
        {
            int damage = GetEffect().remainingCharges * 2;
            Tile oldTile = ((Unit)GetCard()).GetTile();

            if (damage > 0)
            {
                MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                {

                    foreach (Tile tile in oldTile.GetAdjacentTilesLinear())
                    {
                        if (tile.GetUnit() != null)
                        {
                            tile.GetUnit().SufferDamage(minion, minion.activatedAbility, damage);
                        }
                    }

                });

            }
        }

    }
}
