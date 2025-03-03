using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerModifierIncreaseSelfByMatchingTag : PowerModifier
{

    public int increaseAmountPerMatchingTag;
    public CardTag tagToMatch;
    public bool dontCountSelf;

    public override int ModifyPower(Unit unit, int currentPower, Unit attacker, Unit defender)
    {
        int increaseAmount = 0;
        if (unit == GetCard() && unit.GetZone() == MenuControl.Instance.battleMenu.board)
        {
            foreach (Card card in GetCard().player.cardsOnBoard)
            {
                if (card.cardTags.Contains(tagToMatch))
                {
                    if (!dontCountSelf || card != GetCard())
                        increaseAmount += 1;
                }
            }
            return currentPower + increaseAmount;
        }

        return currentPower;
    }
}
