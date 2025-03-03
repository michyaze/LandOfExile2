using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EventChoiceTricksterSpiritTrade : EventChoice
{

    public EventDefinition successDefinition;
    public EventDefinition failDefinition;

    public override void PerformChoice()
    {
        if (MenuControl.Instance.heroMenu.cardsOwned.Count <= 1) CloseEvent();

        Card cardToRemove = null;
        while (cardToRemove == null)
        {
            foreach (Card card in MenuControl.Instance.heroMenu.cardsOwned.OrderByDescending(x => x.level))
            {
                if (!(card is Hero))
                {
                    cardToRemove = card;
                }
            }
        }

        MenuControl.Instance.heroMenu.RemoveCardFromDeck(cardToRemove);

        List<Card> artifacts = MenuControl.Instance.heroMenu.GetArtifacts(true);
        if (artifacts.Count == 0)
        {
            artifacts = MenuControl.Instance.heroMenu.GetArtifacts(false);
        }


        EventDefinition eventDefinition = successDefinition;
        bool scammed = Random.Range(1, 101) <= 50;
        
        Card cardToAdd = artifacts.Count==0?null:artifacts[Random.Range(0, artifacts.Count)];
        if (artifacts.Count == 0)
        {
            scammed = true;
        }
        else
        {
            
        }
        if (scammed)
        {
            eventDefinition = failDefinition;
            //MenuControl.Instance.heroMenu.accumulatedGold += 1;

        }
        else
        {

            MenuControl.Instance.heroMenu.AddCardToDeck(cardToAdd);

        }

        MenuControl.Instance.dataControl.SaveData();

        MenuControl.Instance.cardChoiceMenu.ShowNotifcation(cardToRemove, () =>
        {
            if (!scammed)
                MenuControl.Instance.cardChoiceMenu.ShowNotifcation(cardToAdd, () => { }, MenuControl.Instance.GetLocalizedString("CardAddedToDeckPrompt", "This card has been added to your deck:"));

            MenuControl.Instance.eventMenu.ShowEvent(eventDefinition);


        }, MenuControl.Instance.GetLocalizedString("RemovedFromDeckPrompt"));
    }

}
