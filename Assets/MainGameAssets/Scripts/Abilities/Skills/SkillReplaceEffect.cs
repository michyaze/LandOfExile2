using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillReplaceEffect : Ability
{
    public Effect replacedEffectTemplate;
    public Effect effectTemplate;
    public int charges;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        for (int ii = 0; ii < MenuControl.Instance.heroMenu.hero.startingEffects.Count; ii += 1) {
            Effect effect = MenuControl.Instance.heroMenu.hero.startingEffects[ii];
            if (effect == replacedEffectTemplate)
            {
                MenuControl.Instance.heroMenu.hero.startingEffects.RemoveAt(ii);
                MenuControl.Instance.heroMenu.hero.startingEffectCharges.RemoveAt(ii);
                break;
            }
        }

        MenuControl.Instance.heroMenu.hero.startingEffects.Add(effectTemplate);
        MenuControl.Instance.heroMenu.hero.startingEffectCharges.Add(charges);
    }
}
