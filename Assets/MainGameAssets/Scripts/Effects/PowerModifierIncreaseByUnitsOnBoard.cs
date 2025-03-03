using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerModifierIncreaseByUnitsOnBoard : PowerModifier
{
    public List<Card> cardTemplates;
    public OpposingTargetType targetType;
    public int increaseAmount=1;
    public bool includingSelf = false;

    public override int ModifyPower(Unit unit, int currentPower, Unit attacker, Unit defender)
    {
        if (GetCard() == unit && unit.GetZone() == MenuControl.Instance.battleMenu.board)
        {
            var addAmountInTotal = 0;
            foreach (var unitOnBoard in MenuControl.Instance.battleMenu.GetAllUnitsOnBoard())
            {
                if (!includingSelf)
                {
                    if (unitOnBoard == unit)
                    {
                        continue;
                    }
                }
                if (CardUtils.isOpposingTargetType(unitOnBoard, GetCard(), targetType))
                {
                    bool isCardTemplateValid = true;
                    if (cardTemplates.Count > 0)
                    {
                        isCardTemplateValid = false;
                        foreach (var template in cardTemplates)
                        {
                            if (unitOnBoard.UniqueID == template.UniqueID)
                            {
                                isCardTemplateValid = true;
                                break;
                            }
                        }
                    }

                    if (isCardTemplateValid)
                    {
                        addAmountInTotal += increaseAmount;
                    }
                }
            }

                return currentPower +addAmountInTotal;
        }

        return currentPower;
    }
}
