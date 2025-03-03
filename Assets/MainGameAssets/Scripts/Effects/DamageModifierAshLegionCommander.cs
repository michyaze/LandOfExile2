using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageModifierAshLegionCommander : DamageModifier
{

    public override int ModifyAmount(Card sourceCard, Ability ability, Unit unit, int currentAmount)
    {
        Unit thisUnit = (Unit)GetCard();
        if (thisUnit.GetZone() == MenuControl.Instance.battleMenu.board && ability.GetCard() is Minion && ability.GetEffect() == null)
        {
            if (thisUnit.GetAdjacentTiles().Contains(unit.GetTile()))
            {
                if (unit.player == thisUnit.player)
                {
                    if (unit.GetHP() <= currentAmount)
                    {
                        return unit.GetHP() - 1;
                    }
                }
            }

        }

        return currentAmount;
    }


}
