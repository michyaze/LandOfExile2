using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSilverbackAlpha : Trigger
{
    public override void MinionEvolved(Minion minion)
    {
        if (minion == GetCard())
        {
            //When this minion evolves, give adjacent friendly minions +1/+0.
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                foreach (Tile tile in ((Unit)GetCard()).GetAdjacentTiles())
                {
                    if (tile.GetUnit() != null && tile.GetUnit() is Minion && tile.GetUnit().player == GetCard().player)
                    {

                        tile.GetUnit().ChangePower(this, tile.GetUnit().currentPower + 1);

                    }
                }

            });

        }



    }
}
