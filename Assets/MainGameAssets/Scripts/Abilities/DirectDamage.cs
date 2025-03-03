using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectDamage : Ability
{
    public int damageAmount;
    public bool useChargesAsDamageAmount;
    public bool exhaustTargetIfDestroyed;
    public bool destroyWeapon;
    public bool triggerUnitDamage = true;
    public bool useDamageAmount = false;
    public bool isElementalReaction = false;
    public bool targetTrap = false;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (targetTrap)
        {
            var trap = GetCard() as WeatherTrap;
            if (trap)
            {
                targetTile = trap.GetTile();
            }
        }
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
