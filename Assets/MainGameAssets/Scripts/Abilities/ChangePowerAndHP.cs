using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePowerAndHP : Ability
{
    //攻击时，会消耗目标身上的火附着，如果成功消耗，则自身获得+2/+2,我希望消耗目标的火附着，但是自身获得这个buff
    public bool applyToSelf = false;

    public int changePower = 1;
    public int changeHP = 1;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        var target = targetTile.GetUnit();
        if (applyToSelf)
        {
            target = (Unit)GetCard();
        }

        if (!target)
        {
            return;
        }
        if (changePower != 0)
            target.ChangePower(this, target.currentPower + changePower);

        if (changeHP != 0)
        {
            target.ChangeCurrentHP(this, target.currentHP + changeHP);
            if (changeHP > 0)
            {
                target.ChangeMaxHP(this, target.GetInitialHP() + changeHP);
            }
        }
    }
}

