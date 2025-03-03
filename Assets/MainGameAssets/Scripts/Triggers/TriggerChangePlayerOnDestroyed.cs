using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChangePlayerOnDestroyed : Trigger
{


    public override void MinionDestroyed(Card sourceCard, Ability ability, int damageAmount, Minion minion)
    {
        if (minion == GetCard())
        {
            Minion unit = minion;

            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                unit.player.PutCardIntoZone(unit, MenuControl.Instance.battleMenu.limbo);
                unit.player = unit.player.GetOpponent();
                unit.player.PutCardIntoZone(unit, MenuControl.Instance.battleMenu.discard);


            }, true);


        }
    }
}
