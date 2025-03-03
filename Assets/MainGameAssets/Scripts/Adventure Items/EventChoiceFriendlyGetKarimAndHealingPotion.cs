using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventChoiceFriendlyGetKarimAndHealingPotion : EventChoice
{
    public HeroPath heroPath;
    public int minionCount = 1;
    public int minionLevel = 2;

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
        bool hasEffect = MenuControl.Instance.levelUpMenu.variableTalentsAcquired.Contains(MenuControl.Instance
            .levelUpMenu
            .lessUpgradeCostTalent);

        // if (!hasEffect)
        // {
        //     List<Card> cardsToShow = new List<Card>();
        //
        //     List<string> buttonLabels = new List<string>();
        //     buttonLabels.Add(MenuControl.Instance.GetLocalizedString("OK"));
        //
        //
        //     List<System.Action> actions = new List<System.Action>();
        //     actions.Add(() => { });
        //
        //     MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actions,
        //         MenuControl.Instance.GetLocalizedString("RequiredCard"), 0, 0, true, -1, false);
        // }
        // else
        {
            List<Card> cardsToShow = new List<Card>();
            {

                var pathCards = heroPath.pathCards;
                pathCards = MenuControl.Instance.heroMenu.FilterCardsOfLevel(pathCards, minionLevel);

                while (cardsToShow.Count < minionCount)
                {
                    Card randomCard = pathCards[Random.Range(0, pathCards.Count)];
                    if (!cardsToShow.Contains(randomCard))
                    {
                        cardsToShow.Add(randomCard);
                        MenuControl.Instance.heroMenu.AddCardToDeck(randomCard);
                    }
                }
            }


            {
                List<Card> elligibleCards = new List<Card>();

                foreach (Card card in MenuControl.Instance.heroMenu.heroClass.classCards)
                {
                    if (card is NewWeapon && card.isCardUnlockedAndAvailable && levelValid(card.level))
                    {
                        elligibleCards.Add(card);
                    }
                }

                foreach (Card card in MenuControl.Instance.heroMenu.GetAllLoot())
                {
                    if (card is NewWeapon && card.isCardUnlockedAndAvailable && levelValid(card.level))
                    {
                        elligibleCards.Add(card);
                    }
                }

                Card randomCard = elligibleCards[Random.Range(0, elligibleCards.Count)];
                MenuControl.Instance.heroMenu.AddCardToDeck(randomCard);
                cardsToShow.Add(randomCard);
            }


            MenuControl.Instance.cardChoiceMenu.ShowNotifcation(cardsToShow, () => { CloseEvent(); },
                MenuControl.Instance.GetLocalizedString("CardAddedToDeckPrompt"));


            MenuControl.Instance.dataControl.SaveData();
        }
    }
}