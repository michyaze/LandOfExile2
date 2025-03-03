using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHellfirePact : Trigger
{
    public override void CardPlayed(Card card, Tile tile, List<Tile> tiles)
    {
        if (GetCard().player == card.player)
        {
            if (card.cardTags.Contains(MenuControl.Instance.spellTag) && card.GetCost() > 0 )
            {
                MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                {
                    if (card.GetZone() == MenuControl.Instance.battleMenu.discard)
                        card.DrawThisCard();
                });

            }
        }
    }
}
