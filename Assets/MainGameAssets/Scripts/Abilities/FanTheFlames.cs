using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AbilityAffectType{target, all}
public class FanTheFlames : Ability
{

    public Effect burnEffectTemplate;

    public AbilityAffectType abilityAffectType;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        List<Unit> unitsAffected = new List<Unit>();
        if (abilityAffectType == AbilityAffectType.all)
        {
            
            foreach (Unit unit in MenuControl.Instance.battleMenu.GetAllUnitsOnBoard())
            {
                if (unit.GetEffectsWithTemplate(burnEffectTemplate).Count > 0)
                {
                    unitsAffected.Add(unit);
                }
            }
        }
        else if (abilityAffectType == AbilityAffectType.target)
        {
            var targetUnit = targetTile.GetUnit();
            if (targetUnit!=null)
            {
                if (targetUnit.GetEffectsWithTemplate(burnEffectTemplate).Count > 0)
                {
                    unitsAffected.Add(targetUnit);
                }
            }
            else
            {
                //被攻击的人死了就会这样，很正常
                //Debug.LogError("FanTheFlames target attack should has a unit on the targetTile");
            }
        }

        foreach (Unit unit in unitsAffected)
        {
            unit.SufferDamage(sourceCard, this, unit.GetEffectsWithTemplate(burnEffectTemplate)[0].remainingCharges);
        }
    }
}
