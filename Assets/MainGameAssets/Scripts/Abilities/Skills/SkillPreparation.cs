using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SkillPreparation : Ability
{

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        List<Card> cards = new List<Card>();
        foreach (Card card in MenuControl.Instance.heroMenu.cardsOwned)
        {
            if (card is Castable && card.cardTags.Contains(MenuControl.Instance.spellTag))
            {
                cards.Add(card);
            }
        }

        MenuControl.Instance.heroMenu.startOfBattleHandCardIDs.Clear();
        
        List<string> buttonLabels = new List<string>();
        buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Skip"));
        buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Confirm"));

        List<Action> actions = new List<Action>();

        actions.Add(() => { });
        actions.Add(() =>
        {
            foreach (int integer in MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts)
            {
                Card card = MenuControl.Instance.cardChoiceMenu.cardsToShow[integer];
                MenuControl.Instance.heroMenu.startOfBattleHandCardIDs.Add(card.UniqueID);
            }
            MenuControl.Instance.dataControl.SaveData();
            MenuControl.Instance.levelUpMenu.ContinueLevelUp();
        });

        MenuControl.Instance.cardChoiceMenu.ShowChoice(cards, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("MageBasic02CardDescription"), 1, 1, true, 2, true);

    }
}
