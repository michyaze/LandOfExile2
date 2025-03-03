using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerModifierMatchingTagInPlay : PowerModifier
{
    public int amountToAdd = 1;
    public CardTag cardTag;
    public bool enemyUnits;
    public bool friendlyUnits;

    public override int ModifyPower(Unit unit, int currentPower, Unit attacker, Unit defender)
    {
        int newPower = currentPower;
        if ((enemyUnits && unit.player == GetCard().player.GetOpponent()) || (friendlyUnits && unit.player == GetCard().player))
        {
            if (GetCard() != null && GetCard().GetZone() == MenuControl.Instance.battleMenu.board && (cardTag == null || unit.cardTags.Contains(cardTag)))
            {
                newPower += amountToAdd;
            }
        }

        return newPower;
    }
}
