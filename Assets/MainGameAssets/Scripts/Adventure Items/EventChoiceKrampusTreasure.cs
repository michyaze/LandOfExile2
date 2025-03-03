using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceKrampusTreasure : EventChoice
{
    public List<Card> treasures = new List<Card>();
    public override void PerformChoice()
    {
        
        Card randomCard = treasures[Random.Range(0,treasures.Count)];

        List<Card> cardsToShow = new List<Card>();
        cardsToShow.Add(randomCard);
        
        List<string> buttonLabels = new List<string>();
        buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Skip"));
        buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Confirm"));

        List<System.Action> actions = new List<System.Action>();
        actions.Add(() => {
            CloseEvent();
        });
        actions.Add(()=>{
            MenuControl.Instance.heroMenu.AddCardToDeck(randomCard);
            MenuControl.Instance.dataControl.SaveData();
            CloseEvent();
        });

        MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("ChooseCardToAddPrompt"), 1, 1, false, -1, true);

    }
}
