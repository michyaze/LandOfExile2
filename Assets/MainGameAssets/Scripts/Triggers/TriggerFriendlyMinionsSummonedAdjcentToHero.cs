using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFriendlyMinionsSummonedAdjcentToHero : Trigger
{
    public Ability otherAbilityToPerform;
    public override void MinionSummoned(Minion minion)
    {
        if (GetCard().player == minion.player)
        {
            if (((Unit)GetCard()).GetAdjacentTiles().Contains(minion.GetTile())){

                MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                {
                    if (this !=null && GetCard() != null)
                    {
                        otherAbilityToPerform.PerformAbility(GetCard(), minion.GetTile());
                    }
                });
            }
        }
    }
}
