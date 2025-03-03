using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGestalt : Trigger
{
    public Effect consciousnessEffectTemplate;

    public override void MinionDestroyed(Card sourceCard, Ability ability, int damageAmount, Minion minion)
    {
        if (minion != GetCard() && GetCard().GetZone() == MenuControl.Instance.battleMenu.board)
        {
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                minion.player.GetHero().ApplyEffect(sourceCard, this, consciousnessEffectTemplate, 1);
            });
        }
    }
}
