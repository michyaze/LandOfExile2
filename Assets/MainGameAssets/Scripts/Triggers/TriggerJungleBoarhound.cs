using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerJungleBoarhound : Trigger
{
    public override void MinionDestroyed(Card sourceCard, Ability ability, int damageAmount, Minion minion)
    {
        if (minion == GetCard())
        {
            foreach (Effect effect in minion.currentEffects)
            {
                effect.transform.parent = GetCard().player.GetHero().transform;
                effect.gameObject.AddComponent<TriggerRemoveThisEffectEOT>();
                GetCard().player.GetHero().currentEffects.Add(effect);
            }
            minion.currentEffects.Clear();
        }
    }
}
