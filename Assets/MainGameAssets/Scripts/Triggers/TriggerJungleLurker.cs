using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerJungleLurker : Trigger
{
    public override void UnitChangedPower(Unit unit, Ability ability, int oldValue)
    {
        if (unit == GetCard() && ((Unit)GetCard()).GetPower() > oldValue && unit.GetZone() == MenuControl.Instance.battleMenu.board)
        {
            Tile targetTile = unit.GetTile();
            int damageAmount = unit.GetPower();
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                foreach (Tile tile in targetTile.GetAdjacentTilesLinear())
                {
                    if (tile.GetUnit() && tile.GetUnit().player != unit.player)
                    {
                        tile.GetUnit().SufferDamage(GetCard(), this, damageAmount);
                    }
                }
            });
        }
    }
}
