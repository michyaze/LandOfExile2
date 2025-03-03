using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceKarimRecruiterElder : EventChoice
{

    public HeroPath heroPath;

    public int minionCount = 1;
    public int minionLevel = 3;
    public override void PerformChoice()
    {
        bool hasEffect = true;//MenuControl.Instance.levelUpMenu.variableTalentsAcquired.Contains(MenuControl.Instance.levelUpMenu
           // .lessUpgradeCostTalent);
        
        if (!hasEffect)
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
            List<Card> cardsToShow = new List<Card>();

            var pathCards = heroPath.pathCards;
            pathCards = MenuControl.Instance.heroMenu.FilterCardsOfLevel(pathCards, minionLevel);
            
            while (cardsToShow.Count < minionCount)
            {
                Card randomCard = pathCards[Random.Range(0, pathCards.Count)];
                if (!cardsToShow.Contains(randomCard))
                {
                    cardsToShow.Add(randomCard);
                }
            }
            foreach (Card card in cardsToShow)
            {
                MenuControl.Instance.heroMenu.AddCardToDeck(card);
            }

            MenuControl.Instance.cardChoiceMenu.ShowNotifcation(cardsToShow, () => { CloseEvent(); }, MenuControl.Instance.GetLocalizedString("CardAddedToDeckPrompt"));

            
            MenuControl.Instance.dataControl.SaveData();

        }
        
        
        

    }

}
