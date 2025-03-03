using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TriggerPerformAfterEffectChanged : Trigger
{
    // 施加effect后触发
    public Ability increaseAbility;
    public Ability decreaseAbility;
    public List<Effect> effectTemplates;
    public bool eventMustTriggerOnThisCard = true;
    public OpposingTargetType opposingTargetType;

    public bool targetCurrentCard;
    bool isUnitValid(Unit unit)
    {
        if (eventMustTriggerOnThisCard)
        {
            if (unit.player != GetCard().player)
            {
                return false;
            }
        }

        if (unit is Hero)
        {
            if (unit.player == GetCard().player)
            {
                if ((opposingTargetType & OpposingTargetType.PlayerHero) != OpposingTargetType.PlayerHero)
                {
                    return false;
                }
            }
            else
            {
                if ((opposingTargetType & OpposingTargetType.EnemyBoss) != OpposingTargetType.EnemyBoss)
                {
                    return false;
                }
            }
        }
        else
        {
            if (unit.player == GetCard().player)
            {
                if ((opposingTargetType & OpposingTargetType.PlayerMinion) != OpposingTargetType.PlayerMinion)
                {
                    return false;
                }
            }
            else
            {
                if ((opposingTargetType & OpposingTargetType.EnemyMinion) != OpposingTargetType.EnemyMinion)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public override void EffectChargesChanged(Ability ability, Effect effect, int previousCharges)
    {
        //     base.EffectChargesChanged(ability, effect, previousCharges);
        // }
        //
        // (Unit unit, Ability ability, Effect effect, int charges)
        // {


        Unit unit = effect.GetCard() as Unit;
        
        
        var targetTile = targetCurrentCard? (GetCard() as Unit)?.GetTile():unit.GetTile();
        if (targetTile == null)
        {
            return;
        }
        if (!isUnitValid(unit))
        {
            return;
        }

        foreach (var effectTemplate in effectTemplates)
        {
            if (effect.originalTemplate.UniqueID == effectTemplate.UniqueID)
            {
                var diff = effect.remainingCharges - previousCharges;
                if (diff > 0)
                {
                    increaseAbility.PerformAbility(GetCard(),targetTile ,
                        effect.remainingCharges - previousCharges);
                }
                else if (diff < 0)
                {
                    decreaseAbility.PerformAbility(GetCard(), targetTile,
                        effect.remainingCharges - previousCharges);
                }

                return;
            }
        }
    }

    public override void UnitRemovedEffect(Unit unit, Ability ability, Effect effect)
    {
        if (!isUnitValid(unit))
        {
            return;
        }

        var targetTile = targetCurrentCard? (GetCard() as Unit)?.GetTile():unit.GetTile();
        if (targetTile == null)
        {
            return;
        }
        foreach (var effectTemplate in effectTemplates)
        {
            if (effect.originalTemplate.UniqueID == effectTemplate.UniqueID)
            {
                var diff = effect.remainingCharges;
                decreaseAbility?.PerformAbility(GetCard(), targetTile,
                    diff);


                return;
            }
        }
    }
}