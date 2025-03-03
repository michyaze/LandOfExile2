using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOvercharge : Trigger
{
    public int spellsCastThisTurn = 0;

    public override void TurnStarted(Player player)
    {
        GetComponent<DamageModifierOverCharge>().canTrigger = false;
        spellsCastThisTurn = 0;
        GetEffect().remainingCharges = 0;
    }

    public override void CardPlayed(Card card, Tile tile, List<Tile> tiles)
    {
        if (card.player == GetCard().player)
        {
            spellsCastThisTurn += 1;
            ((Unit)GetCard()).ApplyEffect(GetCard(), this, GetEffect().originalTemplate, 1);
            if (spellsCastThisTurn == 4)
            {
                GetComponent<DamageModifierOverCharge>().canTrigger = true;
            } else
            {
                GetComponent<DamageModifierOverCharge>().canTrigger = false;
            }
        }
    }

}
