using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpellAbsorbtion : Trigger
{
    public override void CardPlayed(Card card, Tile tile, List<Tile> tiles)
    {
        if (tile == ((Unit)GetCard()).GetTile() || (GetCard() is LargeHero largeHero&& largeHero.GetTiles().Contains(tile)))
        {
            //MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            //{
                card.ExhaustThisCard();
                GetComponent<TriggerEnchantment>().cardsToReturn.Add(card);
            //});
        }
    }
}
