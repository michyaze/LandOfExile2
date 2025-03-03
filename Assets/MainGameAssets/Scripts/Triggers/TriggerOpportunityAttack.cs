using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOpportunityAttack : Trigger
{
    public bool canTrigger = true;
    public Ability anotherAbility;
    public override void UnitMoved(Unit unit, Tile originalTile, Tile destinationTile)
    {
        if (GetCard() == null || GetCard().GetZone() != MenuControl.Instance.battleMenu.board) return;

        Unit thisUnit = (Unit)GetCard();

        if (unit != thisUnit && unit.player != thisUnit.player/* && canTrigger*/)
        {
            //thisUnit.GetTile() == originalTile 考虑到交换触发的缠斗，unit已经移动到thisUnit的位置了，thisUnit是从unit现在的位置移动过去的。理论上thisUnit不应该会从unit移动过去，除了交换触发的缠斗
            if (thisUnit.GetAdjacentTiles().Contains(originalTile) || thisUnit.GetTile() == originalTile)
            {
                canTrigger = false;
                MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                {
                    if (unit.GetTile() != null)
                    {
                        anotherAbility.PerformAbility(GetCard(),unit.GetTile());
                        //thisUnit.ForceAttack(unit.GetTile());
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