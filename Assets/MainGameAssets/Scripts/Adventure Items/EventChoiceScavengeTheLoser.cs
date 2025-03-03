using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceScavengeTheLoser : EventChoice
{

    public override void PerformChoice()
    {

        List<Card> elligibleCards = new List<Card>();
        elligibleCards.AddRange(MenuControl.Instance.heroMenu.GetUnlockedSpellCards());
        elligibleCards.AddRange(MenuControl.Instance.heroMenu.GetUnlockedMinionCards());
        elligibleCards.AddRange(MenuControl.Instance.heroMenu.heroClass.classCards);
        elligibleCards.AddRange(MenuControl.Instance.heroMenu.heroPath.pathCards);

        List<Card> elligibleCards2 = new List<Card>();
        elligibleCards2 = MenuControl.Instance.heroMenu.FilterCardsOfLevel(elligibleCards, 2);

        Card card = elligibleCards2[Random.Range(0, elligibleCards2.Count)];

        List<Card> cardsToShow = new List<Card>();
        cardsToShow.Add(card);

        List<System.Action> actions = new List<System.Action>();
        actions.Add(() =>
        {
            CloseEvent();

        });
        actions.Add(() =>
        {
            MenuControl.Instance.heroMenu.AddCardToDeck(card);
            MenuControl.Instance.dataControl.SaveData();
            CloseEvent();

        });

        List<string> buttonStrings = new List<string>();
        buttonStrings.Add(MenuControl.Instance.GetLocalizedString("Cancel"));
        buttonStrings.Add(MenuControl.Instance.GetLocalizedString("Confirm"));

        MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonStrings, actions, MenuControl.Instance.GetLocalizedString("ChooseCardToAddPrompt"), 1, 1, true, 2, true);

    }

}
