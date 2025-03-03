using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ManaType{MaxMana, CurrentMana}
public class PowerModifierIncreaseByMana : PowerModifier
{
    public  ManaType manaType;
    public override int ModifyPower(Unit unit, int currentPower, Unit attacker, Unit defender)
    {
        int res = currentPower;
        int newPower = currentPower;
        if (manaType == ManaType.MaxMana)
        {
            newPower = currentPower + unit.player.initialMana;
        }
        else if (manaType == ManaType.CurrentMana)
        {
            newPower = currentPower + unit.player.GetCurrentMana();
        }
        if (unit == GetCard() && unit.GetZone() == MenuControl.Instance.battleMenu.board)
        {
            
            if (GetComponentInParent<Card>() is NewWeapon weapon)
            {
                if (unit is Hero hero && hero.weapon == weapon)
                {
                    
                    res = newPower;
                }
            }
            else
            {
                res = newPower;

            }
        }
        return res;
    }
}
