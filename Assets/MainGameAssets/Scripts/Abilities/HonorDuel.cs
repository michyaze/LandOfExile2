using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HonorDuel : Ability
{
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        Minion minion = (Minion)targetTile.GetUnit();

        foreach(Tile tile in targetTile.GetAdjacentTilesLinear(1))
        {
            if (tile.GetUnit() != null && tile.GetUnit().player != minion.player && minion.activatedAbility is Attack)
            {
                if (minion.GetZone() == MenuControl.Instance.battleMenu.board && tile.GetUnit().GetZone() == MenuControl.Instance.battleMenu.board)
                {
                    minion.ForceAttack(tile);
                }
            }
        }


        foreach (Tile tile in targetTile.GetAdjacentTilesLinear(1))
        {
            if (tile.GetUnit() != null && tile.GetUnit().player != minion.player && tile.GetUnit().activatedAbility is Attack)
            {
                if (targetTile.GetUnit() != null && targetTile.GetUnit().GetZone() == MenuControl.Instance.battleMenu.board && tile.GetUnit().GetZone() == MenuControl.Instance.battleMenu.board)
                {
                    tile.GetUnit().ForceAttack(targetTile);
                }
            }
        }

    }
}
