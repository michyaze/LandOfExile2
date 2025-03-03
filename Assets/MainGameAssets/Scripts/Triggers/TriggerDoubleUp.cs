using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoubleUp : Trigger
{
    
    public CardList doubleUpSpellTemplates;
    public bool shouldExhaustAfterTrigger = false;
    public override void CardPlayed(Card card, Tile tile, List<Tile> tiles)
    {
        Tile originalTile = tile;
        Card originalCard = card;
        Unit originalUnit = tile.GetUnit();

        bool cardInDoubleUpTemplates = false;
        foreach (Card card2 in doubleUpSpellTemplates.cards)
        {
            if (card2.UniqueID == card.cardTemplate.UniqueID)
            {
                cardInDoubleUpTemplates = true;
            }
        }

        if (card.player == GetCard().player && card is Castable && card.cardTags.Contains(MenuControl.Instance.spellTag) && !cardInDoubleUpTemplates && GetEffect().remainingCharges > 0)
        {

            GetEffect().ConsumeCharges(this, 1);

            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
            // if (originalCard.activatedAbility != null && originalCard.activatedAbility is RepeatableAbility)
            // {
            //     ((RepeatableAbility)originalCard.activatedAbility).timesToRepeat += 1;
            // }
            // else 
            if (tiles != null)
            {
                ((Castable)originalCard).TargetTiles(tiles, false);
                exhaust(originalCard);
            }
            else if (originalUnit == null)
            {
                originalCard.TargetTile(tile, false);
                exhaust(originalCard);
            }
            else if (originalUnit.GetZone() == MenuControl.Instance.battleMenu.board)
            {
                originalCard.TargetTile(originalUnit.GetTile(), false);
                exhaust(originalCard);
            }

        });

        }

    }

    void exhaust(Card originalCard)
    {
        if (shouldExhaustAfterTrigger)
        {
            originalCard.ExhaustThisCard();
        }
    }
}
