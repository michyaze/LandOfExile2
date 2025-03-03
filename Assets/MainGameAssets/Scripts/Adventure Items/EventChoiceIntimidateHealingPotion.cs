using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceIntimidateHealingPotion : EventChoice
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
        bool hasCard = false;
        foreach (Card card in MenuControl.Instance.heroMenu.cardsOwned)
        {
            if (card is Minion && ((Minion)card).GetPower() >= 4)
            {
                hasCard = true;
            }
        }

        if (!hasCard)
        {

            List<Card> cardsToShow = new List<Card>();

            List<string> buttonLabels = new List<string>();
            buttonLabels.Add(MenuControl.Instance.GetLocalizedString("OK"));


            List<System.Action> actions = new List<System.Action>();
            actions.Add(() => { });

            MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("RequiredCard"), 0, 0, true, -1, false);
        }
        else
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
            
            
            MenuControl.Instance.dataControl.SaveData();
            CloseEvent();

        }

    }
}
