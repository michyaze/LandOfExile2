using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceKarimRecruiter : EventChoice
{

    public HeroPath heroPath;

    public override void PerformChoice()
    {
        List<Card> cardsToShow = new List<Card>();
        while (cardsToShow.Count < 1)
        {
            var cards = MenuControl.Instance.heroMenu.heroClass.GetComponent<ClassSpecialization>()
                .RestrictForAreasVisited(heroPath.pathCards);
            if (cards.Count == 0)
            {
                Debug.LogError("EventChoiceKarimRecruiter 中 RestrictForAreasVisited 结果为空");
                cards = heroPath.pathCards;
            }
            Card randomCard = cards[Random.Range(0, cards.Count)];
            if (!cardsToShow.Contains(randomCard))
            {
                cardsToShow.Add(randomCard);
            }
        }
        foreach (Card card in cardsToShow)
        {
            MenuControl.Instance.heroMenu.AddCardToDeck(card);
        }
        MenuControl.Instance.dataControl.SaveData();

        MenuControl.Instance.cardChoiceMenu.ShowNotifcation(cardsToShow, () => { CloseEvent(); }, MenuControl.Instance.GetLocalizedString("CardAddedToDeckPrompt"));


    }

}
