using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRisingTides : Trigger
{
    public int spellsCast;
    public override void TurnEnded(Player player)
    {
        spellsCast = 0;
    }

    public override void CardPlayed(Card card, Tile tile, List<Tile> tiles)
    {
        if (card.player == GetCard().player)
        {
            spellsCast += 1;
            ((DamageModifierRisingTides)GetEffect()).canModify = false;
            if (spellsCast == 3)
            {
                ((DamageModifierRisingTides)GetEffect()).canModify = true;
            }
        }
    }
}
