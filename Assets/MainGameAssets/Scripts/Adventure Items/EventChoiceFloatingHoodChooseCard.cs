using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class EventChoiceFloatingHoodChooseCard : EventChoice
{
    public override void PerformChoice()
    {
        MenuControl.Instance.ApplySeed();
        List<Card> cardsToShow = new List<Card>();
        List<Card> cards = MenuControl.Instance.heroMenu.FilterCardsOfLevel(MenuControl.Instance.heroMenu.heroClass.classCards, 3);
        cards.AddRange(MenuControl.Instance.heroMenu.FilterCardsOfLevel(MenuControl.Instance.heroMenu.heroPath.pathCards, 3));

        int count = Math.Min(3, cards.Count);
        
        while (cardsToShow.Count < count)
        {
            Card newCard = cards[Random.Range(0, cards.Count)];
            if (!cardsToShow.Contains(newCard))
                cardsToShow.Add(newCard);
        }

        List<string> buttonLabels = new List<string>();
        buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Cancel"));
        buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Confirm"));

        List<System.Action> actions = new List<System.Action>();
        actions.Add(() => { });
        actions.Add(() =>
        {
            foreach (int selectedCardInt in MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts)
            {
                Card card = MenuControl.Instance.cardChoiceMenu.visibleCardsShown[selectedCardInt].card;
                MenuControl.Instance.heroMenu.AddCardToDeck(card);
            }

           // MenuControl.Instance.heroMenu.hero.currentHP -= 4;

            //Resets XP to start of this level
            if (MenuControl.Instance.heroMenu.currentLevel == 1)
            {
                MenuControl.Instance.heroMenu.currentXP = 0;
            }
            else
            {
                MenuControl.Instance.heroMenu.currentXP = MenuControl.Instance.heroMenu.levelsXP[MenuControl.Instance.heroMenu.currentLevel - 2];
            }

            MenuControl.Instance.dataControl.SaveData();

            CloseEvent();

            MenuControl.Instance.adventureMenu.ContinueAdventure();

        });

        MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("ChooseCardToAddPrompt"), 1, 1, true, -1, false);

    }
}
