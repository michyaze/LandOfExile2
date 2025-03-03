using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceDwarvenGhostDeck : EventChoice
{
    public EventDefinition nextEvent;
    public int damageAmount = 2;

    public override void PerformChoice()
    {
        List<Card> cardsToShow = new List<Card>();
        for (int ii = 0; ii < 2; ii += 1)
        {
            List<Card> upgradableCards = new List<Card>();
            foreach (Card card in MenuControl.Instance.heroMenu.cardsOwned)
            {
                if (card.RandomUpgradeCard != null)
                {
                    upgradableCards.Add(card);
                }
            }

            if (upgradableCards.Count > 0)
            {

                Card oldCard = upgradableCards[Random.Range(0, upgradableCards.Count)];
                Card newCard = oldCard.RandomUpgradeCard;
                MenuControl.Instance.heroMenu.RemoveCardFromDeck(oldCard);
                MenuControl.Instance.heroMenu.AddCardToDeck(newCard);

                cardsToShow.Add(newCard);
            }
        }

        MenuControl.Instance.heroMenu.hero.SufferDamage(null, null, damageAmount);

        MenuControl.Instance.dataControl.SaveData();
       

        List<string> buttonLabels = new List<string>();
        buttonLabels.Add(MenuControl.Instance.GetLocalizedString("OK"));

        List<System.Action> actions = new List<System.Action>();
        actions.Add(() => {
            MenuControl.Instance.eventMenu.ShowEvent(nextEvent);
        });

        MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("CardsUpgradedInDeckPrompt"), 0, 0, true, -1, false);

    }
}
