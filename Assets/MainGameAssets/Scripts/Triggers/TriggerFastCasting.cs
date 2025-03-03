using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerFastCasting : Trigger
{
    public override void CardPlayed(Card card, Tile tile, List<Tile> tiles)
    {
        if (!(GetCard() is NewWeapon) && (!(GetCard() is Unit) || GetCard().GetZone() != MenuControl.Instance.battleMenu.board)) return;

        if (GetCard().player == card.player) {
            if (card.initialCost > 0)
            {
                if (! GetComponent<CostsModifierFastCasting>().canModifyCard(card))
                {
                    return;
                }
                GetComponent<CostsModifierFastCasting>().firstTriggerInstance = false;
            }
        }
    }
    public override void TurnStarted(Player player)
    {

        if (GetCard().player == player)
        {
            GetComponent<CostsModifierFastCasting>().firstTriggerInstance = true;
        }
    }

}
