using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillApplyEffect : Ability
{
    public Effect effectTemplate;
    public int charges;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (sourceCard != null && sourceCard is Unit unit)
        {
            unit.ApplyEffect(null, null, effectTemplate, charges);
        }
        else
        {
            
            if (effectTemplate.conflictEffects != null && effectTemplate.conflictEffects.Count > 0)
            {
                foreach (Effect conflictEffect in effectTemplate.conflictEffects)
                {
                    if (MenuControl.Instance.heroMenu.hero.startingEffects.Contains(conflictEffect))
                    {
                        MenuControl.Instance.heroMenu.hero.startingEffects.Remove((conflictEffect));
                    }
                }
            }
            if (!MenuControl.Instance.heroMenu.hero.startingEffects.Contains(effectTemplate) || !effectTemplate.chargesStack)
            {
                MenuControl.Instance.heroMenu.hero.startingEffects.Add(effectTemplate);
                MenuControl.Instance.heroMenu.hero.startingEffectCharges.Add(charges);
            }
            else
            {
                MenuControl.Instance.heroMenu.hero.startingEffectCharges[MenuControl.Instance.heroMenu.hero.startingEffects.IndexOf(effectTemplate)] += charges;
            }
        }
    }
}
