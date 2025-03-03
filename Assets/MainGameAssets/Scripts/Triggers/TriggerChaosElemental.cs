using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChaosElemental : Trigger
{
    public override void UnitDamaged(Card sourceCard, Unit unit, Ability ability, int damageAmount,
        bool triggerUnitDamage = true)
    {
        if (unit == GetCard())
        {
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                foreach (Card card in GetCard().player.cardsInHand.ToArray())
                {
                    card.DiscardThisCard();
                }
                foreach (Card cardTemplate in ((Hero)GetCard()).GetIntentSystem().GetNextHand().GetFollowingHandCards())
                {
                    Card newCard = GetCard().player.CreateCardInGameFromTemplate(cardTemplate);
                    newCard.DrawThisCard();
                }
                ((Hero)GetCard()).GetIntentSystem().TurnEnded(GetCard().player);
                GetCard().player.RenderIntent2ndHand();
            });
        }
    }
}
