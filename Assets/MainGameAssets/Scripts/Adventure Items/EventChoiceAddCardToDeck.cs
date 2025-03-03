using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceAddCardToDeck : EventChoice
{
    public Card card;
    public int changeHP;

    public override void PerformChoice()
    {

        if (changeHP > 0)
        {
            MenuControl.Instance.heroMenu.hero.Heal(null, null, changeHP);
        }
        else if (changeHP < 0)
        {
            MenuControl.Instance.heroMenu.hero.currentHP += changeHP;
        }

        MenuControl.Instance.heroMenu.AddCardToDeck(card);
        MenuControl.Instance.dataControl.SaveData();
        MenuControl.Instance.cardChoiceMenu.ShowNotifcation(card, () =>
        {
            CloseEvent();
            MenuControl.Instance.adventureMenu.ContinueAdventure();
        }, MenuControl.Instance.GetLocalizedString("CardAddedToDeckPrompt", "This card has been added to your deck:"));

    }

}
