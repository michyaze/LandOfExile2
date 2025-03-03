using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PsychicAssault : Trigger
{
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        for (int ii = 0; ii < 5; ii += 1)
        {
            GetCard().player.GetOpponent().GetHero().SufferDamage(GetCard(), this, 1);
        }
    }

    public override void UnitDamaged(Card sourceCard, Unit unit, Ability ability, int damageAmount,
        bool triggerUnitDamage = true)
    {
        if (damageAmount > 0 && unit == GetCard().player.GetOpponent().GetHero() && ability == this)
        {
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {

                if (GetCard().player.GetOpponent().cardsInDeck.Count > 0)
                {
                    Card cardToExhaust = GetCard().player.GetOpponent().cardsInDeck[0];
                    cardToExhaust.ExhaustThisCard();

                    if (cardToExhaust is Minion)
                    {
                        Card newCard = GetCard().player.CreateCardInGameFromTemplate(cardToExhaust.cardTemplate);
                        List<Tile> tiles = MenuControl.Instance.battleMenu.boardMenu.GetAllEmptyTiles();
                        
                        if (tiles.Count > 0)
                        {
                            tiles.Shuffle();
                            newCard.TargetTile(tiles[0], false);
                        }
                       
                    }

                }

            });
               
        }
    }
}
