using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infest : Ability
{
    public Effect infectTemplateEffect;
    public int charges = 1;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        Effect effect = targetTile.GetUnit().ApplyEffect(sourceCard, this, infectTemplateEffect, charges);
        if (effect == null)
        {
            return;
        }
        TriggerInfestedSummonToTile summonEffect = effect.GetComponent<TriggerInfestedSummonToTile>();
        if (summonEffect != null)
        {
            summonEffect.templateCard = GetCard().cardTemplate;
        }
    }

}
