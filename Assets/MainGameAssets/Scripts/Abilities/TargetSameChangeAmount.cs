using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum TargetMode
{
    PowerOfUnit, PowerOfMyHero, ChargesOnThisEffect, PowerOfThisUnit, PowerOfWeapon, DirectNumber,HeroHPLost,MaxMana,CurrentMana
}
public class TargetSameChangeAmount : Ability
{

    public Ability otherAbility;
    public TargetMode targetMode;
    public int extraDamage;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (targetMode == TargetMode.PowerOfUnit)
        {
            if (targetTile.GetUnit() != null)
            {
                otherAbility.PerformAbility(sourceCard, targetTile, targetTile.GetUnit().GetPower());
            }
        }
        else if (targetMode == TargetMode.PowerOfMyHero)
        {
            if (targetTile.GetUnit() != null)
            {
                otherAbility.PerformAbility(sourceCard, targetTile, sourceCard.player.GetHero().GetPower());
            }
        }
        else if (targetMode == TargetMode.ChargesOnThisEffect)
        {
            if (targetTile.GetUnit() != null)
            {
                otherAbility.PerformAbility(sourceCard, targetTile, GetEffect().remainingCharges);
            }
        }else if (targetMode == TargetMode.PowerOfThisUnit)
        {
            if (targetTile.GetUnit() != null)
            {
                var unit = GetCard() as Unit;
                if (unit == null)
                {
                    unit = GetCard().player.GetHero();
                }
                otherAbility.PerformAbility(sourceCard, targetTile, unit.GetPower() + extraDamage);
            }
        }else if (targetMode == TargetMode.PowerOfWeapon)
        {
            if (targetTile.GetUnit() != null)
            {
                var damage = extraDamage;
                if (sourceCard.player.GetHero().weapon)
                {
                    damage += sourceCard.player.GetHero().weapon.GetPower();
                }
                otherAbility.PerformAbility(sourceCard, targetTile, damage);
            }
        }else if (targetMode == TargetMode.HeroHPLost)
        {
            if (targetTile == null)
            {
                Debug.LogError("?");
            }
            //if (targetTile!=null && targetTile.GetUnit() != null)
            {
                otherAbility.PerformAbility(sourceCard, targetTile, sourceCard.player.GetHero().GetHPLost());
            }
        }else if (targetMode == TargetMode.MaxMana)
        {
            if (targetTile == null)
            {
                Debug.LogError("?");
            }
            //if (targetTile!=null && targetTile.GetUnit() != null)
            {
                otherAbility.PerformAbility(sourceCard, targetTile, sourceCard.player.GetInitialMana());
            }
        }else if (targetMode == TargetMode.CurrentMana)
        {
            if (targetTile == null)
            {
                Debug.LogError("?");
            }
            //if (targetTile!=null && targetTile.GetUnit() != null)
            {
                otherAbility.PerformAbility(sourceCard, targetTile, sourceCard.player.GetCurrentMana());
            }
        }
    }
}
