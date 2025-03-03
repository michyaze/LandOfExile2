using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectDamageWithHeroCharge : Ability
{
    public int damageAmount;
    public bool useChargesAsDamageAmount;
    public bool exhaustTargetIfDestroyed;
    public bool destroyWeapon;
    public bool triggerUnitDamage = true;
    public bool useDamageAmount = false;
    public List<Effect> effectTemplates;
    
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        
        //Perform standard attack
        if (targetTile == null)
        {
            return;
        }

        if (targetTile.GetUnit() != null)
        {
            Unit unit = targetTile.GetUnit();

            var finalAmount = amount;
            if (useChargesAsDamageAmount)
            {
                finalAmount = GetEffect().remainingCharges;

            }

            if (finalAmount <= 0 || useDamageAmount)
            {
                finalAmount = damageAmount;
                
            }

            foreach (var effectTemplate in effectTemplates)
            {
                if (effectTemplate == null)
                {
                    continue;
                }
                var effects = MenuControl.Instance.battleMenu.player1.GetHero().GetEffectsWithTemplate(effectTemplate);
                if (effects.Count > 0)
                {
                    foreach (var effect in effects)
                    {
                        finalAmount += effect.remainingCharges;
                    }
                }
            }

        unit.SufferDamage(sourceCard, this,finalAmount,false,triggerUnitDamage);

            if (exhaustTargetIfDestroyed && unit.GetZone() != MenuControl.Instance.battleMenu.board)
            {
                unit.ExhaustThisCard();
            }

            if (destroyWeapon)
            {
                var hero = sourceCard.player.GetHero();
                if (hero.weapon)
                {
                    hero.weapon.UnequipWeapon(hero,false);
                }
            }
        }

    }


}
