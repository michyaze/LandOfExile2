using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainFireblast : RepeatableAbility
{

    public int damageAmount = 3;

    public int chosenManaAmount;
    public Tile tileToHit;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        List<Card> cardsToShow = new List<Card>();
        for (int ii = 0; ii < sourceCard.player.GetCurrentMana(); ii += 1)
        {
            cardsToShow.Add(sourceCard);
        }

        List<string> buttonLabels = new List<string>();
        buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Confirm"));

        timesToRepeat = 1;

        List<System.Action> actions = new List<System.Action>();
        actions.Add(() => {
            chosenManaAmount = MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts.Count;
            tileToHit = targetTile;

            GetCard().player.ChangeMana(-chosenManaAmount);

            RepeatAbility();

            MenuControl.Instance.battleMenu.ProcessTriggeredAbilities();

        });

        MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("SelectXPrompt"), 1, cardsToShow.Count, false, -1, false);
    }

    public override void RepeatAbility()
    {

        for (int ii = 0; ii < chosenManaAmount; ii += 1)
        {
            if (tileToHit.GetUnit() != null)
                tileToHit.GetUnit().SufferDamage(GetCard(), this, damageAmount);
        }

        base.RepeatAbility();
    }
}
