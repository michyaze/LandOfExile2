using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRetaliate : Trigger
{
    public override void UnitAttacks(Unit attacker, Unit defender, Tile targetTile, bool initialAttack)
    {
        if (GetCard() == defender && initialAttack)
        {
            if (attacker.GetZone() == MenuControl.Instance.battleMenu.board  && GetCard().GetZone() == MenuControl.Instance.battleMenu.board && ((Unit)GetCard()).GetHP() > 0 && ((Unit)GetCard()).GetPower() > 0)
            {
                MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                {
                    if (attacker.GetZone() == MenuControl.Instance.battleMenu.board && defender.GetZone() == MenuControl.Instance.battleMenu.board && ((Unit)GetCard()).GetHP() > 0 && ((Unit)GetCard()).GetPower() > 0)
                    {
                        bool canAttack = false;
                        if (defender.activatedAbility != null && defender.CanAct() && defender.activatedAbility.CanTargetTile(defender, attacker.GetTile()))
                        {
                            canAttack = true;
                        }

                        if (attacker is LargeHero largeHero)
                        {
                            foreach (Tile tile2 in largeHero.GetTiles())
                            {
                                if (defender.activatedAbility != null && defender.activatedAbility.CanTargetTile(defender, tile2))
                                {
                                    canAttack = true;
                                }
                            }
                        }

                        if (canAttack)
                        {
                            defender.ForceAttack(attacker.GetTile());
                        }
                    }

                });
            }
        }
    }
}
