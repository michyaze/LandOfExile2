using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ChangeWeaponPowerDuality : Ability
{
    public int changePower;
    public int changeDuality;
    public int maxValue = Int32.MaxValue;
    

    public bool useAmountInstead;  

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (useAmountInstead) changePower = amount;
        changePower = Math.Min(maxValue, changePower);
        if (targetTile == null || targetTile.GetUnit() == null||targetTile.GetUnit().player.GetHero() == null)
        {
            return;
        }
            var weapon = targetTile.GetUnit().weapon;
            if (!weapon)
            {
                return;
            }
            weapon.ChangePower(this, weapon.GetPower() + changePower);
            weapon.ChangeDuality(this, targetTile.GetUnit(),weapon.GetDuality() + changeDuality);

    }
}
