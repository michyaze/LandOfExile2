using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePower : Ability
{
    public int changePower;

    public bool includeSameTeamUnitsAdjacent;
    public bool useAmountInstead;  

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (useAmountInstead) changePower = amount;

        if (targetTile==null || targetTile.GetUnit() == null)
        {
            //probably the performer is dead, example Red103
            //Debug.LogError("targetTile is empty");
            return;
        }
        
        targetTile.GetUnit().ChangePower(this, targetTile.GetUnit().currentPower + changePower);


        if (includeSameTeamUnitsAdjacent)
        {
            Tile tile = targetTile.GetTileLeft();
            if (tile != null && tile.GetUnit() != null && tile.GetUnit().player == targetTile.GetUnit().player)
            {
                tile.GetUnit().ChangePower(this, tile.GetUnit().currentPower + changePower);

            }

            tile = targetTile.GetTileRight();
            if (tile != null && tile.GetUnit() != null && tile.GetUnit().player == targetTile.GetUnit().player)
            {
                tile.GetUnit().ChangePower(this, tile.GetUnit().currentPower + changePower);

            }

            tile = targetTile.GetTileUp();
            if (tile != null && tile.GetUnit() != null && tile.GetUnit().player == targetTile.GetUnit().player)
            {
                tile.GetUnit().ChangePower(this, tile.GetUnit().currentPower + changePower);

            }

            tile = targetTile.GetTileDown();
            if (tile != null && tile.GetUnit() != null && tile.GetUnit().player == targetTile.GetUnit().player)
            {
                tile.GetUnit().ChangePower(this, tile.GetUnit().currentPower + changePower);

            }
        }

    }
}
