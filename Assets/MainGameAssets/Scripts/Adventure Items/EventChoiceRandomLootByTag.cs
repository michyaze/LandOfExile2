using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceRandomLootByTag : EventChoice
{
    public CardTag cardTag;

    public override void PerformChoice()
    {
        List<Card> cards = new List<Card>();
        foreach (Card card in MenuControl.Instance.heroMenu.allCards)
        {
            if (card.cardTags.Contains(MenuControl.Instance.lootTag) && card.cardTags.Contains(cardTag))
            {
                cards.Add(card);
            }
        }

        Card randomCard = cards[Random.Range(0, cards.Count)];
        List<Card> cardsToShow = new List<Card>();
        cardsToShow.Add(randomCard);

        List<string> buttonLabels = new List<string>();
        buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Cancel"));
        buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Confirm"));

        List<System.Action> actions = new List<System.Action>();
        actions.Add(() => { CloseEvent(); });
        actions.Add(() => {
          
            List<VisibleCard> vcsToAnimate = new List<VisibleCard>();
            foreach (int selectedCardInt in MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts)
            {
                Card card = MenuControl.Instance.cardChoiceMenu.visibleCardsShown[selectedCardInt].card;
                MenuControl.Instance.heroMenu.AddCardToDeck(card);
                vcsToAnimate.Add(MenuControl.Instance.cardChoiceMenu.visibleCardsShown[selectedCardInt]);
            }
            MenuControl.Instance.adventureMenu.AnimateVCsToDeck(vcsToAnimate);

            CloseEvent(); });

        MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("LootPrompt"), 1, cardsToShow.Count, true, 2, true);
    }

}
