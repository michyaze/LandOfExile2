using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AncientCoin : Ability
{
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        sourceCard.player.DrawACard();

        MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
        {

            List<Card> cardsToShow = new List<Card>();
            cardsToShow.AddRange(sourceCard.player.cardsInHand);

            if (cardsToShow.Count == 0) return;

            List<string> buttonLabels = new List<string>();
            buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Confirm"));

            List<System.Action> actions = new List<System.Action>();
            actions.Add(() =>
            {

                for (int ii = 0; ii < MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts.Count; ii += 1)
                {
                    Card card = MenuControl.Instance.cardChoiceMenu.cardsToShow[MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts[ii]];
                    sourceCard.player.cardsInHand.Remove(card);
                    sourceCard.player.cardsInDeck.Insert(0, card);
                }

                MenuControl.Instance.battleMenu.ProcessTriggeredAbilities();

            });

            MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("SelectCardToPutOnTopOfDeck"), 1, 1, false, -1, false);

        });
    }
}
