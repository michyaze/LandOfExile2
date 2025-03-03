using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManthriCouncillor : Trigger
{
    public CardTag councillorTag;

    public override void UnitDamaged(Card sourceCard, Unit unit, Ability ability, int damageAmount,
        bool triggerUnitDamage = true)
    {
        if (unit == GetCard() && unit.GetHP() <= 0)
        {
            foreach (Card card in unit.player.cardsOnBoard)
            {
                if (card.cardTags.Contains(councillorTag) && card != unit)
                {
                    Unit otherUnit = (Unit)card;
                    if (otherUnit.GetHP() > damageAmount)
                    {
                        otherUnit.SufferDamage(GetCard(), this, damageAmount);
                        unit.Heal(GetCard(), this, damageAmount);
                        MenuControl.Instance.battleMenu.GetComponent<InBattleDialogueController>().ShowBattleDialogue("300480");
                        return;
                    }
                }
            }
        }
    }
}
