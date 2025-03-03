using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleManaModifier : ManaModifier
{
    public int modifier;

    public override int ModifyAmount(Player player, int currentAmount)
    {
        if (player == GetCard().player)
        {
            if (GetCard() is Unit)
            {
                if (((Unit)GetCard()).GetZone() != MenuControl.Instance.battleMenu.board)
                {
                    return currentAmount;
                }
            }
            return currentAmount + modifier * (GetComponent<Effect>().chargesStack? GetComponent<Effect>().remainingCharges:1);
        }
        return currentAmount;
    }
}
