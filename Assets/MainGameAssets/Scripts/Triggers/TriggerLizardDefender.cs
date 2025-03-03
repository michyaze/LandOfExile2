using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLizardDefender : Trigger
{
    public Effect powerEffectTemplate;
    public int charges;
    public Card eggTemplate;

    public override void MinionDestroyed(Card sourceCard, Ability ability, int damageAmount, Minion minion)
    {
        if (GetCard().GetZone() == MenuControl.Instance.battleMenu.board)
        {
            if (minion.cardTemplate.UniqueID == eggTemplate.UniqueID && minion.GetZone() == MenuControl.Instance.battleMenu.board)
            {
                Unit unit = (Unit)GetCard();
                MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                {
                    unit.ApplyEffect(GetCard(), this, powerEffectTemplate, charges);
                });

            }
        }
    }
}
