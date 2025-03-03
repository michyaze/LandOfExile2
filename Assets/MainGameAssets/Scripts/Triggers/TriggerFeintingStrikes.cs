using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFeintingStrikes : Trigger
{

    public Block blockEffectTemplate;

    public override void UnitAttacks(Unit attacker, Unit defender, Tile targetTile, bool initialAttack)
    {
        Hero hero = (Hero)GetCard();
        if (attacker == hero)
        {
            // feint strike can only apply on 1 range attack, why? I don't know.
            if (hero.weapon != null /*&& hero.weapon.activatedAbility.targetValidator is TargetLinear && ((TargetLinear)hero.weapon.activatedAbility.targetValidator).range == 1*/)
            {
                MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                {
                    hero.ApplyEffect(GetCard(), this, blockEffectTemplate, GetEffect().remainingCharges);
                });
            }
        }
    }
}
