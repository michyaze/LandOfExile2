using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyRandomCardInHand : Ability
{
    public bool permanently;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (sourceCard.player.cardsInHand.Count > 0)
        {

            Card handCard = sourceCard.player.cardsInHand[Random.Range(0, sourceCard.player.cardsInHand.Count)];

            Card newCard = sourceCard.player.CreateCardInGameFromTemplate(handCard.cardTemplate);

            newCard.DrawThisCard();

            if (permanently)
            {
                MenuControl.Instance.heroMenu.AddCardToDeck(MenuControl.Instance.heroMenu.GetCardByID(newCard.cardTemplate.UniqueID));
                //MenuControl.Instance.dataControl.SaveData();
            }
        }
    }
}
