using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformWithCountOfUnitWithEffect : Ability
{
    public List<Effect> effectTemplates;
    public List<Effect> tileEffectTemplate;
    public Ability anotherAbility;
    public OpposingTargetType opposingTargetType;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        int res = 0;
        foreach (var unit in MenuControl.Instance.battleMenu.GetAllUnitsOnBoard())
        {
            foreach (var effect in effectTemplates)
            {
                if (CardUtils.isOpposingTargetType(unit, this, opposingTargetType))
                {
                    if (unit.GetEffectsWithTemplate(effect).Count > 0)
                    {
                        res += 1;
                    }
                }
            }
        }

        if ((opposingTargetType & OpposingTargetType.Tile) == OpposingTargetType.Tile)
        {
            foreach (Tile tile in MenuControl.Instance.battleMenu.boardMenu.tiles)
            {
                foreach (var effect in tileEffectTemplate)
                {
                    if (tile.GetEffectsWithTemplate(effect).Count > 0)
                    {
                        res++;
                        break;
                    }
                }
            }
        }

        if (res > 0)
        {
            anotherAbility.PerformAbility(sourceCard, targetTile, res);
        }
    }
}