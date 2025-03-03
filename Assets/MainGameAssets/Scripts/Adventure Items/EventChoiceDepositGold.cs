using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this whole event is deprecated as there is no gold or treasure change to gold
public class EventChoiceDepositGold : EventChoice
{

    public override void PerformChoice()
    {
        // //Build choice menu
        // List<string> buttonLabels = new List<string>();
        // buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Cancel"));
        // buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Convert"));
        //
        // List<System.Action> actions = new List<System.Action>();
        // actions.Add(() =>
        // {
        //
        // });
        // actions.Add(() => {
        //
        //     List<VisibleCard> vcsToAnimate = new List<VisibleCard>();
        //     foreach (int selectedCardInt in MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts)
        //     {
        //         Card card = MenuControl.Instance.cardChoiceMenu.visibleCardsShown[selectedCardInt].card;
        //         MenuControl.Instance.heroMenu.RemoveCardFromDeck(card);
        //
        //         int goldToGive = card.goldWorth;
        //         if (MenuControl.Instance.heroMenu.easyMode)
        //         {
        //             goldToGive = Mathf.CeilToInt(goldToGive / 2f);
        //         }
        //         
        //         //MenuControl.Instance.heroMenu.accumulatedGold += goldToGive;
        //         //MenuControl.Instance.heroMenu.goldConvertedThisRun += goldToGive;
        //     }
        //     //MenuControl.Instance.adventureMenu.AnimateVCsToDeck(vcsToAnimate); TODO animate removal
        //
        //     MenuControl.Instance.dataControl.SaveData();
        //
        //     MenuControl.Instance.eventMenu.CloseMenu();
        //     MenuControl.Instance.adventureMenu.ContinueAdventure();
        // });
        //
        // List<Card> cardsToShow = MenuControl.Instance.heroMenu.GetTreasureCardsOwned(true);
        // MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("ConvertGoldPrompt"), 1, cardsToShow.Count, true, false, true);

    }

}
