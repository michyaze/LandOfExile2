using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillIncreaseInitialMana : Ability
{
    public int amountToIncrease = 1;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        MenuControl.Instance.heroMenu.initialMana += amountToIncrease;
    }
}
