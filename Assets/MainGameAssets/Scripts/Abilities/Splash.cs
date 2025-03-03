using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : Ability
{

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        Tile tile = targetTile.GetTileLeft();
        if (tile != null && tile.GetUnit() != null && tile.GetUnit().player != sourceCard.player)
        {
            tile.GetUnit().SufferDamage(sourceCard, this, ((Unit)sourceCard).GetPower((Unit)sourceCard, targetTile.GetUnit()));
        }

        tile = targetTile.GetTileRight();
        if (tile != null && tile.GetUnit() != null && tile.GetUnit().player != sourceCard.player)
        {
            tile.GetUnit().SufferDamage(sourceCard, this, ((Unit)sourceCard).GetPower((Unit)sourceCard, targetTile.GetUnit()));
        }

        tile = targetTile.GetTileUp();
        if (tile != null && tile.GetUnit() != null && tile.GetUnit().player != sourceCard.player)
        {
            tile.GetUnit().SufferDamage(sourceCard, this, ((Unit)sourceCard).GetPower((Unit)sourceCard, targetTile.GetUnit()));
        }

        tile = targetTile.GetTileDown();
        if (tile != null && tile.GetUnit() != null && tile.GetUnit().player != sourceCard.player)
        {
            tile.GetUnit().SufferDamage(sourceCard, this, ((Unit)sourceCard).GetPower((Unit)sourceCard, targetTile.GetUnit()));
        }


    }

}
