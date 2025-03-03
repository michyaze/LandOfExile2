using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceGainWeapon : EventChoice
{
    public int weaponMinLevel = -1;
    public int weaponLevel = 2;

    bool levelValid(int cardLevel)
    {
        if (weaponMinLevel == -1)
        {
            return cardLevel == weaponLevel;
        }
        else
        {
            return cardLevel <= weaponLevel && cardLevel >= weaponMinLevel;
        }
    }
    public override void PerformChoice()
    {
        
        List<Card> potentialCards = new List<Card>();
        foreach (Card card in MenuControl.Instance.heroMenu.cardsOwned)
        {
            if (card.level==1)
            {
                potentialCards.Add(card);
            }
        }

        if (potentialCards.Count>0)
        {
            
            List<Card> elligibleCards = new List<Card>();

            foreach (Card card in MenuControl.Instance.heroMenu.heroClass.classCards)
            {
                if (card is NewWeapon && levelValid(card.level) )
                {
                    elligibleCards.Add(card);
                }
            }
            foreach (Card card in MenuControl.Instance.heroMenu.GetAllLoot())
            {
                if (card is NewWeapon && levelValid(card.level))
                {
                    elligibleCards.Add(card);
                }
            }

            Card randomCard = elligibleCards[Random.Range(0, elligibleCards.Count)];
            MenuControl.Instance.heroMenu.AddCardToDeck(randomCard);
        
            Card cardToRemove = potentialCards[Random.Range(0, potentialCards.Count)];
        
        
            MenuControl.Instance.heroMenu.RemoveCardFromDeck(cardToRemove);
            MenuControl.Instance.dataControl.SaveData();
            MenuControl.Instance.cardChoiceMenu.ShowNotifcation(cardToRemove,
                () =>
                {
                    MenuControl.Instance.cardChoiceMenu.ShowNotifcation(randomCard, () => { CloseEvent(); },
                        MenuControl.Instance.GetLocalizedString("CardAddedToDeckPrompt"));
                }, MenuControl.Instance.GetLocalizedString("RemovedFromDeckPrompt"));
        }
        else
        {
            List<Card> cardsToShow = new List<Card>();

            List<string> buttonLabels = new List<string>();
            buttonLabels.Add(MenuControl.Instance.GetLocalizedString("OK"));


            List<System.Action> actions = new List<System.Action>();
            actions.Add(() => { });

            MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("RequiredCard"), 0, 0, true, -1, false);
        }
        
    }

}
