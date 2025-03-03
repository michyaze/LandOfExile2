using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardWeaponAndApplyEffect : Ability
{
    public Ability applyEffect;

    public int discardWeaponCount = 2;
    public override bool CanTargetTile(Card card, Tile tile)
    {
        return base.CanTargetTile(card, tile) && weaponsInHand().Count>0;
    }

    List<NewWeapon> weaponsInHand()
    {
        
        List<NewWeapon> weaponsInHand = new List<NewWeapon>();
        foreach (Card card1 in GetCard().player.cardsInHand)
        {
            if (card1 is NewWeapon)
            {
                weaponsInHand.Add((NewWeapon)card1);
            }
        }

        return weaponsInHand;
    }
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        var weapons = weaponsInHand();
        List<NewWeapon> weaponsSelected = new List<NewWeapon>();
        var increaseDamage = 0;
        if (weapons.Count <= discardWeaponCount)
        {
            weaponsSelected = weapons;
        }
        else
        {
            NewWeapon randomWeapon = weapons[Random.Range(0, weapons.Count)];
            weaponsSelected.Add(randomWeapon);
            weapons.Remove(randomWeapon);
        }

        foreach (var weapon in weaponsSelected)
        {
            increaseDamage += weapon.GetPower();
            weapon.DiscardThisCard();
        }
        //apply effect
        applyEffect.PerformAbility(sourceCard,targetTile,increaseDamage);
    }
}