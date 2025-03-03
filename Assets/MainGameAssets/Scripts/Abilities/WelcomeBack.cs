using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeBack : Ability
{
    public int numberOfChosenMinions;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        List<Minion> minionsToSummon = new List<Minion>();
        List<Minion> minions = new List<Minion>();
        foreach (Card card in GetCard().player.cardsInDiscard)
        {
            if (card is Minion)
            {
                minions.Add((Minion)card);
            }
        }
        if (numberOfChosenMinions == 0)
        {
            if (minions.Count > 0)
            {
                minionsToSummon.Add(minions[Random.Range(0, minions.Count)]);
                SummonMinions(minionsToSummon);
            }
        }
        else
        {
            //Card choice to select numberOfChosenMinions from minions
            List<Card> cardsToShow = new List<Card>();
            cardsToShow.AddRange(minions);

            if (cardsToShow.Count == 0) return;

            List<string> buttonLabels = new List<string>();
            buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Cancel"));
            buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Confirm"));

            List<System.Action> actions = new List<System.Action>();
            actions.Add(() => { });
            actions.Add(() =>
            {

                List<Minion> selectedMinions = new List<Minion>();
                for (int ii = 0; ii < MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts.Count; ii += 1)
                {
                    Card card = MenuControl.Instance.cardChoiceMenu.cardsToShow[MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts[ii]];
                    selectedMinions.Add((Minion)card);
                }

                SummonMinions(selectedMinions);
                
            });

            MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("SelectCardsToSummon"), 1, numberOfChosenMinions, true, -1, false);
        }
    }

    void SummonMinions(List<Minion> minionsToSummon)
    {
        foreach (Minion minion in minionsToSummon)
        {

            //Find a random empty square
            List<Tile> emptyTiles = MenuControl.Instance.battleMenu.boardMenu.GetAllEmptyTiles();
            if (emptyTiles.Count > 0)
            {
                Tile tile = emptyTiles[Random.Range(0, emptyTiles.Count)];
                if (numberOfChosenMinions == 0)
                {
                    minion.TargetTile(tile, false);
                }
                else
                {
                    MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                    {
                        minion.TargetTile(tile, false);
                    });
                }
            }

        }

        if (minionsToSummon.Count > 0) MenuControl.Instance.battleMenu.ProcessTriggeredAbilities();
    }
}
