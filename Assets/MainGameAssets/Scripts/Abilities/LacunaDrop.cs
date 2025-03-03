using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LacunaDrop : Ability
{
    public Effect lacunaEffectTemplate;
    public int damagePerLacuna = 10;
    public bool targetHero = true;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        int timesCast = 1;
        if (GetCard().player.GetHero().GetEffectsWithTemplate(lacunaEffectTemplate).Count > 0)
        {
            timesCast += GetCard().player.GetHero().GetEffectsWithTemplate(lacunaEffectTemplate)[0].remainingCharges;
        }
        for (int ii = 0; ii < timesCast; ii += 1)
        {
            if (targetHero)
            {
                if (GetCard().player.GetOpponent().GetHero() == null)
                {
                    break;
                }
                GetCard().player.GetOpponent().GetHero().SufferDamage(GetCard(), this, damagePerLacuna);
            }
            else
            {
                if (targetTile.GetUnit() == null)
                {
                    break;
                }
                targetTile.GetUnit().SufferDamage(GetCard(), this, damagePerLacuna);
            }
        }
        GetCard().player.GetHero().ApplyEffect(GetCard(), this, lacunaEffectTemplate, 1);
    }
}
