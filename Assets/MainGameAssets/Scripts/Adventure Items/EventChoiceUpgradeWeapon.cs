using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EventChoiceUpgradeWeapon : EventChoice
{


    public override void PerformChoice()
    {

        List<Card> cardsToShow = new List<Card>();
        foreach (Card card in MenuControl.Instance.heroMenu.weaponsOwned.OrderBy(x => (x.GetName())))
        {
            if (card.RandomUpgradeCard != null)
                cardsToShow.Add(card);
        }

        List<System.Action> actions = new List<System.Action>();
        actions.Add(() =>
        {

        });
        actions.Add(() =>
        {
            Card card = MenuControl.Instance.cardChoiceMenu.cardsToShow[MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts[0]];
            MenuControl.Instance.heroMenu.RemoveCardFromDeck(card);
            MenuControl.Instance.heroMenu.AddCardToDeck(card.RandomUpgradeCard);
            MenuControl.Instance.dataControl.SaveData();
            CloseEvent();

        });

        List<string> buttonStrings = new List<string>();
        buttonStrings.Add(MenuControl.Instance.GetLocalizedString("Cancel"));
        buttonStrings.Add(MenuControl.Instance.GetLocalizedString("Confirm"));

        MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonStrings, actions, MenuControl.Instance.GetLocalizedString("UpgradeWeaponPrompt"), 1, 1, true, 2, true);

    }
}
