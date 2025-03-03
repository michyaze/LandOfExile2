using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyMinion : Ability
{

    public int copies = 1;

    public bool forEnemy;
    public bool putIntoHand = true;
    public bool topOfDeck;
    public bool bottomOfDeck;
    public bool addCopyTrigger = true;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        Card templateToUse = targetTile.GetUnit().cardTemplate;
       
        for (int ii = 0; ii < copies; ii += 1)
        {

            Card newCard = forEnemy ? GetCard().player.GetOpponent().CreateCardInGameFromTemplate(templateToUse) : GetCard().player.CreateCardInGameFromTemplate(templateToUse);
            if (addCopyTrigger) newCard.gameObject.AddComponent<TriggerCopyExhausts>();
            if (putIntoHand)
            {
                newCard.DrawThisCard();
            }
            else if (topOfDeck)
            {
                newCard.player.cardsInDeck.Insert(0, newCard);
            }
            else
            {
                newCard.PutIntoZone(MenuControl.Instance.battleMenu.deck);
            }

        }
    }
}
