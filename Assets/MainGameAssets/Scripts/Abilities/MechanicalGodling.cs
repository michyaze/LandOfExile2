using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicalGodling : Ability
{
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        List<Card> cardsToShow = new List<Card>();
        for (int ii = 0; ii < 4; ii += 1)
        {
            if (sourceCard.player.GetOpponent().cardsInDeck.Count > ii)
            {
                cardsToShow.Add(sourceCard.player.GetOpponent().cardsInDeck[ii]);
            }
        }

        
        if (cardsToShow.Count > 0)
        {
            int cardsToBePicked = 1;
            if (cardsToShow.Count == 1) cardsToBePicked = 1;

            List<string> buttonLabels = new List<string>();
            buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Confirm"));

            List<System.Action> actions = new List<System.Action>();
            actions.Add(() => {

                foreach (int integer in MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts)
                {
                    MenuControl.Instance.cardChoiceMenu.cardsToShow[integer].ExhaustThisCard();
                }
                MenuControl.Instance.battleMenu.ProcessTriggeredAbilities();

            });

            MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("ExhaustFromDeckPrompt"), cardsToBePicked, cardsToBePicked, false, -1, false);
        }
    }
}
