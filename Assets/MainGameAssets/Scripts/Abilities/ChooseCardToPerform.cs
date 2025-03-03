using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCardToPerform : Ability
{
    public List<Card> cardsToShow;
    public string ChoiceMenuTitle;
    
    public override float PerformAnimationTime(Card sourceCard)
    {
        return float.PositiveInfinity;
    }
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        if (cardsToShow.Count == 0) return;

        List<string> buttonLabels = new List<string>();
        buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Confirm"));

        List<System.Action> actions = new List<System.Action>();
        actions.Add(() => {

            
            for (int ii = 0; ii < MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts.Count; ii += 1)
            {
                MenuControl.Instance.cardChoiceMenu
                        .cardsToShow[MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts[ii]].player =
                    sourceCard.player;
                MenuControl.Instance.cardChoiceMenu.cardsToShow[MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts[ii]].activatedAbility.PerformAbility(sourceCard, targetTile, amount);
            }

            MenuControl.Instance.battleMenu.ProcessTriggeredAbilities();

        });
        MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actions, MenuControl.Instance.GetLocalizedString(ChoiceMenuTitle), 1, 1, false, -1, false);

    }
}
