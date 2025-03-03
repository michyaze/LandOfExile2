using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReapersScythe : Ability
{

    public int amountToIncrease = 1;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        Unit unit = targetTile.GetUnit();
        unit.SufferDamage(GetCard(), this, GetCard().player.GetHero().GetPower());

        if (unit.GetZone() != MenuControl.Instance.battleMenu.board)
        {
            GetCard().player.GetHero().ChangePower(this, GetCard().player.GetHero().currentPower + amountToIncrease);
        }
    }
}
