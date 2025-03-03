using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldFortification : Trigger
{
    public override void UnitDamaged(Card sourceCard, Unit unit, Ability ability, int damageAmount,
        bool triggerUnitDamage = true)
    {
        if (((Unit)GetCard()).GetZone() == MenuControl.Instance.battleMenu.board && unit != GetCard())
        {
            if (((Unit)GetCard()).GetTile().GetAdjacentTilesLinear(1).Contains(unit.GetTile()))
            {
                if (unit.cardTemplate.UniqueID != GetCard().cardTemplate.UniqueID)
                {
                    ((Unit)GetCard()).SufferDamage(GetCard(), this, Mathf.FloorToInt(damageAmount / 2f));
                }
            }
        }
    }
}
