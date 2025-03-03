using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMana : Ability
{
    public int amountToChange;
    public bool permanently;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        targetTile.GetUnit().player.ChangeMana(amount == 0 ? amountToChange : amount);
        if (permanently)
        {
            targetTile.GetUnit().player.initialMana += amount == 0 ? amountToChange : amount;
            if (targetTile.GetUnit().player.initialMana < 0) targetTile.GetUnit().player.initialMana = 0;
        }
    }
}
