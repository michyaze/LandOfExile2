using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBearCultSupplicant : Trigger
{
    public override void UnitAppliedEffect(Unit unit, Ability ability, Effect effect, int charges)
    {
        if (GetCard().GetZone() == MenuControl.Instance.battleMenu.board && unit != GetCard() && unit is Minion && unit.cardTemplate.UniqueID != GetCard().cardTemplate.UniqueID)
        {
            if (effect.GetComponent<TriggerEnchantment>() != null && ability.GetCard().player == GetCard().player)
            {
                Effect effectTemplate = effect.originalTemplate;
                Card thisCard = GetCard();
                List<Card> cardsToReturn = new List<Card>();
                cardsToReturn.AddRange(effect.GetComponent<TriggerEnchantment>().cardsToReturn);

                MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                {
                    Effect newEffect = ((Minion)GetCard()).ApplyEffect(thisCard, this, effectTemplate, effect.remainingCharges);
                    if (newEffect == null)
                    {
                        return;
                    }
                    newEffect.GetComponent<TriggerEnchantment>().cardsToReturn.AddRange(cardsToReturn);
                });
            }
        }
    }
}
