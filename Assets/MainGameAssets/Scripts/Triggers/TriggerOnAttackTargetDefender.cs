using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerOnAttackTargetDefender : Trigger
{
    public bool thisUnitIsOnBoard = true;
    public Ability abilityToPerform;

    public override void UnitAttacks(Unit attacker, Unit defender, Tile targetTile, bool initialAttack)
    {
        if (attacker == GetCard())
        {
            if (thisUnitIsOnBoard)
            {
                if (GetCard().GetZone() != MenuControl.Instance.battleMenu.board) return;
            }
            Tile originalTile = defender.GetTile();
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                abilityToPerform.PerformAbility(attacker, defender.GetTile() == null ? originalTile : defender.GetTile());
            });

        }
    }
}
