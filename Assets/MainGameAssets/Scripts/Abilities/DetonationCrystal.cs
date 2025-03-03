using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// is this ever used?
public class DetonationCrystal : Trigger
{
    public Knockback knockbackAbility;

    public override void UnitAttacks(Unit attacker, Unit defender, Tile targetTile, bool initialAttack)
    {

        if (!(GetCard() is NewWeapon) && (!(GetCard() is Unit) || GetCard().GetZone() != MenuControl.Instance.battleMenu.board)) return;

        if (defender == GetCard() && attacker.GetZone() == MenuControl.Instance.battleMenu.board)
        {

            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                knockbackAbility.knockbackAmount = attacker.GetPower();
                knockbackAbility.PerformAbility(attacker, defender.GetTile(), knockbackAbility.knockbackAmount);
            });
        }

    }
}
