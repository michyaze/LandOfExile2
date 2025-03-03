using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoubleEdgedHourglass : Trigger
{
    public override void CardPlayed(Card card, Tile tile, List<Tile> tiles)
    {
        if (card.player == GetCard().player)
        {
            if (card.initialCost > 0)
            {
                card.player.GetHero().ChangeCurrentHP(this, card.player.GetHero().GetHP() - card.initialCost);
            }
        }
    }
}
