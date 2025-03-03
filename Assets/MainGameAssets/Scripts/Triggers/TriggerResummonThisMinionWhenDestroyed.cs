using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerResummonThisMinionWhenDestroyed : Trigger
{

    public override void MinionDestroyed(Card sourceCard, Ability ability, int damageAmount, Minion minion)
    {
        if (GetCard() == minion)
        {
            Tile targetTile = minion.GetTile();

            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                if (targetTile.isMoveable())
                {
                    minion.TargetTile(targetTile, false);
                }
            });
        }
    }

}
