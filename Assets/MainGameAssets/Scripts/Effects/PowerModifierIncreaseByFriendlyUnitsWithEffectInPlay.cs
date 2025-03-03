using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerModifierIncreaseByFriendlyUnitsWithEffectInPlay : PowerModifier
{
    public List<Effect> effects;
    public override int ModifyPower(Unit unit, int currentPower, Unit attacker, Unit defender)
    {
        if (GetCard() == unit && unit.GetZone() == MenuControl.Instance.battleMenu.board)
        {
            int res = 0;
            foreach (var card in GetCard().player.cardsOnBoard)
            {
                if (card is Unit cardUnit)
                {
                    foreach (var effect in effects)
                    {
                        if (cardUnit.GetEffectsByID(effect.UniqueID).Count > 0)
                        {
                            res++;
                            break;
                        }
                    }
                }
            }
            return currentPower + res;
        }

        return currentPower;
    }
}
