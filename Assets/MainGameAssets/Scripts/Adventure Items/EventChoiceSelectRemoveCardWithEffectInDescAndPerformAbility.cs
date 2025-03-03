using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceSelectRemoveCardWithEffectInDescAndPerformAbility : EventChoice
{
    public Effect effect;

    public override void PerformChoice()
    {
        List<Card> cardsToShow = new List<Card>();

        foreach (var card in MenuControl.Instance.heroMenu.cardsOwned)
        {
            if (card.GetDescription().Contains(effect.GetName()))
            {
                cardsToShow.Add(card);
            }
        }
        List<string> buttonLabels = new List<string>();
        buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Cancel"));
        buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Confirm"));

        List<System.Action> actions = new List<System.Action>();
        actions.Add(() => { });
        actions.Add(() =>
        {

            for (int ii = 0; ii < MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts.Count; ii += 1)
            {
                MenuControl.Instance.heroMenu.RemoveCardFromDeck(MenuControl.Instance.cardChoiceMenu.cardsToShow[MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts[ii]]);
                
            }
            performAnotherAbility.PerformAbility(null,null,0);
            MenuControl.Instance.dataControl.SaveData();
            CloseEvent();
        });
        MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("SelectACardPrompt"), 1, 1, true, 1, false);

        
    }

}
