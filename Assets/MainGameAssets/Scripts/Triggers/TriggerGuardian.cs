using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGuardian : Trigger
{
    public bool canTrigger = true;

    public override void UnitMoved(Unit unit, Tile originalTile, Tile destinationTile)
    {
        if (GetCard() == null || GetCard().GetZone() != MenuControl.Instance.battleMenu.board) return;

        Unit thisUnit = (Unit)GetCard();

        if (unit != thisUnit && unit.player != thisUnit.player && canTrigger)
        {
            if (thisUnit.GetAdjacentTiles().Contains(destinationTile) ||
                unit.GetAdjacentTiles().Contains(thisUnit.GetTile()))
            {
                canTrigger = false;
                MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                {
                    if (unit.GetTile() != null)
                    {
                        thisUnit.ForceAttack(unit.GetTile());
                        // if (GetComponentInParent<NewWeapon>() != null)
                        // {
                        //     GetComponentInParent<NewWeapon>().UseWeapon(thisUnit as Hero);
                        // }
                    }
                    else
                    {
                        canTrigger = true;
                    }
                }, true);
            }
        }
    }

    public override void MinionSummoned(Minion minion)
    {
        //getcard = null means this is a weapon in hand, ignore
        if (GetCard() == null || GetCard().GetZone() != MenuControl.Instance.battleMenu.board) return;

        Unit thisUnit = (Unit)GetCard();

        if (minion != thisUnit && minion.player != thisUnit.player && canTrigger)
        {
            if (thisUnit.GetAdjacentTiles().Contains(minion.GetTile()))
            {
                canTrigger = false;
                MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                {
                    if (minion.GetTile() != null)
                    {
                        int actions = thisUnit.remainingActions;
                        thisUnit.TargetTile(minion.GetTile(), false);
                        thisUnit.remainingActions = actions;
                        
                        // if (GetComponentInParent<NewWeapon>() != null)
                        // {
                        //     GetComponentInParent<NewWeapon>().UseWeapon(thisUnit as Hero);
                        // }
                    }
                    else
                    {
                        canTrigger = true;
                    }
                }, true);
            }
        }
    }

    public override void TurnEnded(Player player)
    {
        canTrigger = true;
    }
}