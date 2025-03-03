using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerJungleSpearmouth : Trigger
{
    public override void UnitChangedPower(Unit unit, Ability ability, int oldValue)
    {
        if (unit == GetCard() && ((Unit) GetCard()).GetPower() >= 5 && oldValue < 5 &&
            unit.GetZone() == MenuControl.Instance.battleMenu.board)
        {
            List<Tile> emptyTiles = new List<Tile>();
            foreach (Tile tile in unit.GetTile().GetAdjacentTilesLinear())
            {
                if (tile.isMoveable())
                {
                    emptyTiles.Add(tile);
                }
            }

            if (emptyTiles.Count > 0)
            {
                Tile targetTile = emptyTiles[Random.Range(0, emptyTiles.Count)];
                MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                {
                    if (targetTile.isMoveable())
                    {
                        Card newCopy = GetCard().player.CreateCardInGameFromTemplate(GetCard().cardTemplate);
                        newCopy.TargetTile(targetTile, false);
                    }
                });
            }
        }
    }
}