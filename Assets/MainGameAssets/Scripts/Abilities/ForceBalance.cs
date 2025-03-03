using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceBalance : Ability
{
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        foreach (Unit unit in MenuControl.Instance.battleMenu.GetAllUnitsOnBoard())
        {
            if (unit is Minion)
            {
                unit.ChangePower(this, unit.GetHP());
            }
        }
    }
}
