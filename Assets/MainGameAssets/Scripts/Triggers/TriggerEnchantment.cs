using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnchantment : Trigger
{
    public List<Card> cardsToReturn = new List<Card>();

    public override void MinionDestroyed(Card sourceCard, Ability ability, int damageAmount, Minion minion)
    {
        if (minion == GetCard())
        {
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {

                foreach (Card card in cardsToReturn)
                {
                    card.DrawThisCard();
                }

            });
        }
    }
}
