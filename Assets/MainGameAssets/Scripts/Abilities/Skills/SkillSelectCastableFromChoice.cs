using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SkillSelectCastableFromChoice : Ability
{
    public int numberOfChoices;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {


        List<Card> levelCards = new List<Card>();
        levelCards.AddRange(MenuControl.Instance.heroMenu.FilterCardsOfLevel( MenuControl.Instance.heroMenu.heroClass.classCards,Mathf.Min(3,MenuControl.Instance.areaMenu.areasVisited)));
        List<Card> eligibleCards = new List<Card>();
        foreach (Card card in levelCards)
        {
            if (card is Castable)
            {
                eligibleCards.Add(card);
            }
        }

        List <Card> cards = new List<Card>();
        for (int ii = 0; ii < numberOfChoices; ii += 1)
        {
            cards.Add(eligibleCards[UnityEngine.Random.Range(0, eligibleCards.Count)]);
        }

        List<string> buttonLabels = new List<string>();
        buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Skip"));
        buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Confirm"));

        List<Action> actions = new List<Action>();

        actions.Add(() => { });
            actions.Add(() => {
            foreach (int integer in MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts)
            {
                MenuControl.Instance.heroMenu.AddCardToDeck(MenuControl.Instance.cardChoiceMenu.visibleCardsShown[integer].card);
            }
            MenuControl.Instance.dataControl.SaveData();
        });

        MenuControl.Instance.cardChoiceMenu.ShowChoice(cards, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("ChooseCardToAddPrompt", "Choose a card to add to your deck:"), 1, 1, true, 2, true);


    }
}
