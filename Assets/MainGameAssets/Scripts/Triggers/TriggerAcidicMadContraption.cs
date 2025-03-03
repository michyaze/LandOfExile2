using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAcidicMadContraption : Trigger
{

    public int damageAmount = 5;
    public override void UnitMoved(Unit unit, Tile originalTile, Tile destinationTile)
    {
        if (GetCard().GetZone() != MenuControl.Instance.battleMenu.board) return;

        Unit thisUnit = (Unit)GetCard();

        if (unit != thisUnit && unit.player != thisUnit.player)
        {

            if (thisUnit.GetAdjacentTiles().Contains(destinationTile) || thisUnit.GetAdjacentTiles().Contains(originalTile))
            {

                MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                {
                    if (unit.GetTile() != null && ((Unit)GetCard()).GetTile() != null)
                    {
                        unit.SufferDamage(GetCard(), this, damageAmount);
                        ((Unit)GetCard()).SufferDamage(GetCard(), this, 0, true);
                    }
                }, true);
            }

        }
    }

   
}
