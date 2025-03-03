using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoicePotionsTakeTwo : EventChoice
{
    public int potionCount = 1;
    public override void PerformChoice()
    {
        int seed = MenuControl.Instance.ApplySeed();
        Random.InitState(seed + MenuControl.Instance.adventureMenu.currentMapTileIndex);

        List<Card> potions = new List<Card>();

        foreach (Card card in MenuControl.Instance.heroMenu.CurrentHeroPotionCards())
        {
           // if (card.cardTags.Contains(MenuControl.Instance.potionTag))
            {
                potions.Add(card);
            }
        }

        List<Card> cardsToShow = new List<Card>();

        for (int ii = 0; ii < potionCount; ii += 1)
        {
            Card card = potions[Random.Range(0, potions.Count)];
            cardsToShow.Add(card);

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
                MenuControl.Instance.heroMenu.AddCardToDeck(MenuControl.Instance.cardChoiceMenu.cardsToShow[MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts[ii]]);

            }

            MenuControl.Instance.dataControl.SaveData();
            MenuControl.Instance.adventureMenu.RenderScreen();
            CloseEvent();
        });

        MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("LootPrompt"), 1, cardsToShow.Count, true, -1, false);


    }

}
