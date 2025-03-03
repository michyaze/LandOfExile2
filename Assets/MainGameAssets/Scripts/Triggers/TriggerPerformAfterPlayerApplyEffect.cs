using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Flags]
public enum OpposingTargetType
{
    None = 0,
    Tile = 1<<0,
    EnemyMinion = 1<<1,
    EnemyBoss = 1<<2,
    PlayerMinion = 1<<3,
    PlayerHero = 1<<4,
};

public class TriggerPerformAfterPlayerApplyEffect : Trigger
{



    // 施加effect后触发
    public Ability otherAbility;
    public List<Effect> effectTemplates;
    public bool eventMustTriggerOnThisCard = true;
    [FormerlySerializedAs("applyEffectType")] public OpposingTargetType opposingTargetType;
    public bool onlyWhenEffectAppliedSuccessfully = true;

    public override void UnitAppliedEffectBeforeCheck(Unit unit, Ability ability, Effect effect,int charges)
    {
        if (onlyWhenEffectAppliedSuccessfully)
        {
            return;
        }
          
        UnitAppliedEffectInternal(unit,ability,effect,charges);
    }
    
    public override void UnitAppliedEffect(Unit unit, Ability ability, Effect effect, int charges)
    {
        if (!onlyWhenEffectAppliedSuccessfully)
        {
            return;
        }
        UnitAppliedEffectInternal(unit,ability,effect,charges);
    }

    void UnitAppliedEffectInternal(Unit unit, Ability ability, Effect effect, int charges)
    {
        if (GetCard() is Unit cardUnit)
        {
            if (GetCard().GetZone() != MenuControl.Instance.battleMenu.board) return;
        }
        if (eventMustTriggerOnThisCard)
        {
            if (ability==null || ability.GetCard() == null || ability.GetCard().player != GetCard().player)
            {
                return;
            }
        }

        if (unit is Hero)
        {
            if (unit.player == GetCard().player)
            {
                if ((opposingTargetType & OpposingTargetType.PlayerHero) != OpposingTargetType.PlayerHero)
                {
                    return;
                }
            }
            else
            {
                if ((opposingTargetType & OpposingTargetType.EnemyBoss) != OpposingTargetType.EnemyBoss)
                {
                    return;
                }
            }
        }
        else
        {
            if (unit.player == GetCard().player)
            {
                if ((opposingTargetType & OpposingTargetType.PlayerMinion) != OpposingTargetType.PlayerMinion)
                {
                    return;
                }
            }
            else
            {
                if ((opposingTargetType & OpposingTargetType.EnemyMinion) != OpposingTargetType.EnemyMinion)
                {
                    return;
                }
            }
        }
        

        foreach (var effectTemplate in effectTemplates)
        {
            if (effect.UniqueID == effectTemplate.UniqueID)
            {
                otherAbility.PerformAbility(GetCard(),unit.GetTile(),charges);
                return;
            }
        }
    }

    public override void TileAppliedEffect(Tile tile, Ability ability, Effect effect, int charges)
    {
        if (eventMustTriggerOnThisCard)
        {
            if (ability.GetCard().player != GetCard().player)
            {
                return;
            }
        }

        if ((opposingTargetType & OpposingTargetType.Tile) != OpposingTargetType.Tile)
        {
            return;
        }
        

        foreach (var effectTemplate in effectTemplates)
        {
            if (effect.UniqueID == effectTemplate.UniqueID)
            {
                otherAbility.PerformAbility(GetCard(),tile,charges);
                return;
            }
        }
    }
}
