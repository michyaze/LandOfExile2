using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceInsertKeyFloor4 : EventChoice
{

    

    public override void PerformChoice()
    {
        Card crescentMoonKey = MenuControl.Instance.levelUpMenu.strangeKeyForFloor4;

        if (MenuControl.Instance.heroMenu.DeckContainsCardTemplate(crescentMoonKey))
        {
            Card card = MenuControl.Instance.heroMenu.GetDeckCardByID(crescentMoonKey.UniqueID);
            MenuControl.Instance.heroMenu.RemoveCardFromDeck(card);

            MenuControl.Instance.dataControl.SaveData();
            CloseEvent();

            List<Card> cardsToShow = new List<Card>();
            cardsToShow.Add(crescentMoonKey);

            List<string> buttonLabels = new List<string>();
            buttonLabels.Add(MenuControl.Instance.GetLocalizedString("OK"));

            List<System.Action> actions = new List<System.Action>();
            actions.Add(() => { });

            MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("RemovedFromDeckPrompt"), 0, 0, true, -1, false);

        }
        else
        {

            MenuControl.Instance.ShowBlockingNotification(null, MenuControl.Instance.GetLocalizedString("LockedFloor4DoorNoKeyPromptCardName"), MenuControl.Instance.GetLocalizedString("LockedFloor4DoorNoKeyPromptCardDescription"), () => { });

        }

    }

}
