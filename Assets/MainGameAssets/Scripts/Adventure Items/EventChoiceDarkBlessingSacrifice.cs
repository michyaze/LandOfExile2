using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceDarkBlessingSacrifice : EventChoice
{
    public int xpGain;
    public int cardsToLose = 3;

    public override void PerformChoice()
    {
        List<Card> cardsRemoved = new List<Card>();
        List<Card> cardsInDeck = new List<Card>();
        cardsInDeck.AddRange(MenuControl.Instance.heroMenu.cardsOwned);
        cardsInDeck.Remove(MenuControl.Instance.heroMenu.hero);


        int halfDeck = Mathf.Min(cardsInDeck.Count, cardsToLose);

        for (int ii = 0; ii < halfDeck; ii += 1)
        {
            Card card = cardsInDeck[Random.Range(0, cardsInDeck.Count)];
            if (card != null)
            {
                cardsInDeck.Remove(card);
                cardsRemoved.Add(card);
            }
        }

        foreach (Card card in cardsRemoved)
        {
            MenuControl.Instance.heroMenu.RemoveCardFromDeck(card);
        }

        if (xpGain > 0)
        {
            MenuControl.Instance.heroMenu.AddXP(xpGain);
        }

        MenuControl.Instance.dataControl.SaveData();
        CloseEvent();

        MenuControl.Instance.cardChoiceMenu.ShowNotifcation(cardsRemoved, () => { MenuControl.Instance.adventureMenu.ContinueAdventure(); }, MenuControl.Instance.GetLocalizedString("RemovedFromDeckPrompt"));

    }

}
