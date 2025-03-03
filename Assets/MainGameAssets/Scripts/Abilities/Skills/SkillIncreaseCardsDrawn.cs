using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillIncreaseCardsDrawn : Ability
{
    public int amountToIncrease = 1;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        MenuControl.Instance.heroMenu.drawsPerTurn += amountToIncrease;
    }
}
