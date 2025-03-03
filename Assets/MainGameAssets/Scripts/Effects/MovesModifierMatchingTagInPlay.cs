using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovesModifierMatchingTagInPlay : MovesModifier
{
    public int amountToAdd = 1;
    public CardTag cardTag;
    public bool enemyUnits;
    public bool friendlyUnits;

    public override int ModifyAmount(Unit unit, int currentAmount)
    {
        int newPower = currentAmount;
        if ((enemyUnits && unit.player == GetCard().player.GetOpponent()) || (friendlyUnits && unit.player == GetCard().player))
        {
            if (GetCard() != null && GetCard().GetZone() == MenuControl.Instance.battleMenu.board && (cardTag == null || unit.cardTags.Contains(cardTag)))
            {
                newPower += amountToAdd;
            }
        }

        return newPower;
    }
    
    // public override void DoActionWhenApply(Card sourceCard, Tile targetTile, int amount = 0)
    // {
    //     DoActionWhenApplyWithCardTag(sourceCard, targetTile, cardTag, enemyUnits, friendlyUnits, amount);
    // }
}
