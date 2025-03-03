using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : CollectibleItem
{
    [HideInInspector]
    public int remainingCharges;
    public Effect originalTemplate;
    public bool isPositive;
    public bool chargesStack;

    public bool canBeRemoved = true;

    [HideInInspector]
    public int lastApplyTurnCount;
    
    public bool isVisibleOnCard = true;

    //public Ability abilityBeforeApplyForcePerform;
    public Ability abilityBeforeApply;
    public Ability abilityWhenApply;
    
    
    public List<Effect> conflictEffects;
    public Ability abilityWhenConflict;// 如果有冲突的effect，做这个
    public bool stopApplyWhenConflict = false;//如果有冲突，是否继续effect


    public bool hasConflict()
    {
        var unit = GetCard() as Unit;if (!unit) return false;
        foreach (var currentEffect in unit.currentEffects)
        {
            
            if (currentEffect.conflictEffects.Count > 0)
            {
                foreach (var effect in currentEffect.conflictEffects)
                {
                
                    if (effect.UniqueID == UniqueID)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    public virtual void DoActionWhenConflict(Card sourceCard, Tile targetTile, int amount = 0)
    {
        abilityWhenConflict?.PerformAbility(sourceCard,targetTile,amount);
    }
    public bool CanApply(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (abilityBeforeApply)
        {
            if (!abilityBeforeApply.CanTargetTile(sourceCard, targetTile))
            {
                return false;
            }
        }
        return true;
    }

    // public virtual void DoActionBeforeApplyForcePerform(Card sourceCard, Tile targetTile, int amount = 0)
    // {
    //     abilityBeforeApplyForcePerform?.PerformAbility(sourceCard,targetTile,amount);
    // }
    public virtual void DoActionBeforeApply(Card sourceCard, Tile targetTile, int amount = 0)
    {
        abilityBeforeApply?.PerformAbility(sourceCard,targetTile,amount);
    }
    public virtual void DoActionWhenApply(Card sourceCard, Tile targetTile, int amount = 0)
    {
        abilityWhenApply?.PerformAbility(sourceCard,targetTile,amount);
    }
    public  void DoActionWhenApplyWithCardTag(Card sourceCard, Tile targetTile,CardTag cardTag, bool enemyUnits, bool friendlyUnits,int amount = 0)
    {
        foreach (Unit unit in MenuControl.Instance.battleMenu.GetAllUnitsOnBoard())
        {
            if (unit != null)
            {
                if (cardTag == null || unit.cardTags.Contains(cardTag))
                {
                    if ((enemyUnits && unit.player == GetCard().player.GetOpponent()) ||
                        (friendlyUnits && unit.player == GetCard().player))
                    {
                        if (GetCard() != null && GetCard().GetZone() == MenuControl.Instance.battleMenu.board)
                        {
                            abilityWhenApply?.PerformAbility(sourceCard,unit.GetTile(),amount);
                        }
                    }
                }
            }
        }
    }

    public bool IsEffectPersistent()
    {
        var unit = GetComponentInParent<Unit>();
        if (unit)
        {
            
            var persistentEffects = unit.GetEffectsOfType<PersistenEffect>();
            if (persistentEffects != null)
            {
                foreach (Effect persistentEffect in persistentEffects)
                {
                    if (((PersistenEffect)persistentEffect).effectTemplate.UniqueID == UniqueID)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }
    public void ConsumeCharges(Ability ability, int number = 1)
    {
        if (remainingCharges > 0)
        {
            int previousCharges = remainingCharges;
            remainingCharges -= number;
            if (remainingCharges <= 0)
            {
                GetComponentInParent<Unit>().RemoveEffect(GetComponentInParent<Unit>(), ability, this);
            }
            else
            {
                //Trigger Effect Charges Changed event
                foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
                {
                    try
                    {
                        trigger.EffectChargesChanged(ability, this, previousCharges);
                    }
                    catch (System.Exception e)
                    {
                        Debug.LogError(e);
                    }

                }
            }
        }
    }

    public Card GetCard()
    {
        // if (GetComponentInParent<Card>() is DeprecatedWeapon) 
        //     return transform.parent.GetComponentInParent<Card>();
        if (GetComponentInParent<Card>() is NewWeapon) 
            return transform.parent.GetComponentInParent<Card>();

        return GetComponentInParent<Card>();
    }
}
