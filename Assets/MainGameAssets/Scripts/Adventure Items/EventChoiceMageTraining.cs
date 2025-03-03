using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceMageTraining : EventChoice
{

    public HeroClass mageClass;

    public override void PerformChoice()
    {

        List<Card> cardsToShow = new List<Card>();
        while (cardsToShow.Count < 3)
        {
            Card randomCard = mageClass.classCards[Random.Range(0, mageClass.classCards.Count)];
            if (!cardsToShow.Contains(randomCard) && randomCard.cardTags.Contains(MenuControl.Instance.spellTag))
            {
                cardsToShow.Add(randomCard);
            }
        }


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
            MenuControl.Instance.dataControl.SaveData();
            CloseEvent();
        });

        MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("ChooseCardToAddPrompt"), 1, 1, true, 2, true);

    }

}
