using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWeapon : Trigger
{
    public override void GameStarted()
    {
        var thisWeapon = GetComponent<NewWeapon>();
    }

    public override void WeaponChanged(NewWeapon weaponTemplate, NewWeapon oldWeapon, Unit hero)
    {
        var thisWeapon = GetComponent<NewWeapon>();
        //when equip weapon, there is a copy on player
        if (oldWeapon!=null && oldWeapon == thisWeapon)
        {
            //unequip weapon
            thisWeapon.UnequipWeapon(hero, true);
        }
    }

    public override void AfterUnitAttacks(Unit attacker, Unit defender, Tile targetTile, bool initialAttack, bool consumeWeapon)
    {
        if (!consumeWeapon)
        {
            return;
        }
        var thisWeapon = GetComponent<NewWeapon>();
        if (attacker.weapon!=null &&  attacker.weapon == thisWeapon)
        {
            //unequip weapon
            thisWeapon.UseWeapon(attacker);
        }
    }
}