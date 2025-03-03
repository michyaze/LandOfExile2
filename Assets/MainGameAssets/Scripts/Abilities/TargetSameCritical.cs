using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSameCritical : Ability
{
    public Ability otherAbilityToPerform;
    public bool immediately;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        if (targetTile != null && targetTile.GetUnit())
        {

            Effect markedEffect = null;
            foreach (Effect effect in targetTile.GetUnit().currentEffects)
            {
                if (effect.UniqueID == "RogueEffect01")
                {
                    markedEffect = effect;
                    break;
                }
            }

            if (markedEffect != null)
            {
                markedEffect.ConsumeCharges(this, 1);

                for (int ii = 0; ii < 1+GetCard().player.GetHero().GetEffectsByID("RogueMid02").Count; ii += 1)
                {
                    if (immediately)
                    {
                        otherAbilityToPerform.PerformAbility(sourceCard, targetTile, amount);
                    }
                    else
                    {
                        MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                        {
                            otherAbilityToPerform.PerformAbility(sourceCard, targetTile, amount);
                        });
                    }
                }
            }
        }
    }
}
