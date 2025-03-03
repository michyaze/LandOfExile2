using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRandomCard : Ability
{
    public List<Card> cardTemplates;
    public int copies = 1;

    public bool forEnemy;
    public bool putIntoHand = true;
    public bool permanently;
    public bool addCopyEffect;
    public override float PerformAnimationTime(Card sourceCard)
    {
        return MenuControl.Instance.battleMenu.GetPlaySpeed() * copies * 2;
    }

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        Card templateToUse = cardTemplates.RandomItem();
        if (templateToUse.cardTemplate != null)
        {
            templateToUse = templateToUse.cardTemplate;
        }
       
        for (int ii = 0; ii < copies; ii += 1)
        {

            Card newCard = forEnemy ? GetCard().player.GetOpponent().CreateCardInGameFromTemplate(templateToUse) : GetCard().player.CreateCardInGameFromTemplate(templateToUse);

            if (addCopyEffect) newCard.gameObject.AddComponent<TriggerCopyExhausts>();

            if (putIntoHand)
            {
                newCard.DrawThisCard();
            }
            else
            {
                newCard.PutIntoZone(MenuControl.Instance.battleMenu.deck);
                
                if (MenuControl.Instance.battleMenu.inBattle && MenuControl.Instance.battleMenu.finishedSetup)
                {
                    foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
                    {
                        try
                        {
                            trigger.CardAddedIntoDeck(newCard);
                        }
                        catch (Exception e)
                        {
                            Debug.LogError(e);
                        }

                    }
                }
            }

            if (permanently && MenuControl.Instance.battleMenu.player1 == newCard.player)
            {
                MenuControl.Instance.heroMenu.AddCardToDeck(templateToUse);
            }

        }
    }
}
