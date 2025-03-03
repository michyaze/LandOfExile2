using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvolve : Trigger
{
    public bool hasTriggered;
    

    public override void MinionDestroyed(Card sourceCard, Ability ability, int damageAmount, Minion minion)
    {
        if ((ability.GetCard() == GetCard() || sourceCard == GetCard()) && GetCard().GetZone() == MenuControl.Instance.battleMenu.board && minion != GetCard())
        {
            bool canTrigger = true;
            foreach (TriggerEvolve trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<TriggerEvolve>())
            {
                if (trigger.hasTriggered) canTrigger = false;
            }

            if (canTrigger)
            {
                hasTriggered = true;
                MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                {
                    if (!MenuControl.Instance.cardChoiceMenu.gameObject.activeInHierarchy)
                    {
                        List<Card> cardsToShow = MenuControl.Instance.heroMenu.FilterCardsWithTag(MenuControl.Instance.heroMenu.allCards, MenuControl.Instance.evolveBlessingTag);
                        cardsToShow.Shuffle();
                        if (cardsToShow.Count > 3)
                        {
                            cardsToShow.RemoveRange(3, cardsToShow.Count - 3);
                        }

                        List<string> buttonLabels = new List<string>();
                        buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Confirm"));

                        List<System.Action> actions = new List<System.Action>();
                        actions.Add(() =>
                        {
                            hasTriggered = false;
                            for (int ii = 0; ii < MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts.Count; ii += 1)
                            {
                                Card selectedCard = MenuControl.Instance.cardChoiceMenu.cardsToShow[MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts[ii]];
                                selectedCard.activatedAbility.PerformAbility(GetCard(), GetCard().player.GetHero().GetTile());
                                MenuControl.Instance.battleMenu.ProcessTriggeredAbilities();
                            }

                        });

                        MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("EvolvePrompt"), 1, 1, false, -1, false);
                    }

                }, true);

            }

            
        }
    }

}
