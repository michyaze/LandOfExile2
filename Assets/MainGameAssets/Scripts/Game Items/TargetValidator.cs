using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetValidator : ScriptableObject
{
    public List<Effect> effectTemplates;
    public List<Effect> effectToNotTargetTemplates;
    public virtual bool CanUnitTargetTile(Unit unit, Tile tile)
    {
        return false;
    }

    public string UniqueID;

    public virtual string GetName()
    {
        return MenuControl.Instance.GetLocalizedString(UniqueID);
    }

    public virtual bool hasEffect(Unit otherUnit)
    {
        if (effectTemplates!=null && effectTemplates.Count > 0)
        {
            bool hasEffect = false;
            foreach (var effectTemplate in effectTemplates)
            {
                foreach (var effect in otherUnit.currentEffects)
                {
                    if (effect.originalTemplate == effectTemplate)
                    {
                        hasEffect = true;
                        break;
                    }   
                }

                if (hasEffect)
                {
                    break;
                }

            }

            if (!hasEffect)
            {
                return false;
            }
        }

        return true;
    }
    
    public virtual bool hasEffect(Tile tile)
    {
        if (effectTemplates!=null && effectTemplates.Count > 0)
        {
            bool hasEffect = false;
            foreach (var effectTemplate in effectTemplates)
            {
                foreach (var effect in tile.currentEffects)
                {
                    if (effect.originalTemplate == effectTemplate)
                    {
                        hasEffect = true;
                        break;
                    }   
                }

                if (hasEffect)
                {
                    break;
                }

            }

            if (!hasEffect)
            {
                return false;
            }
        }

        return true;
    }
    
    public virtual bool hasEffectToNotTarget(Unit otherUnit)
    {
        if (effectToNotTargetTemplates!=null && effectToNotTargetTemplates.Count > 0)
        {
            bool hasEffect = false;
            foreach (var effectTemplate in effectToNotTargetTemplates)
            {
                foreach (var effect in otherUnit.currentEffects)
                {
                    if (effect.originalTemplate == effectTemplate)
                    {
                        hasEffect = true;
                        break;
                    }   
                }

                if (hasEffect)
                {
                    break;
                }

            }

            if (!hasEffect)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        return false;
    }
    
    public virtual bool hasEffectToNotTarget(Tile tile)
    {
        if (effectToNotTargetTemplates!=null && effectToNotTargetTemplates.Count > 0)
        {
            bool hasEffect = false;
            foreach (var effectTemplate in effectToNotTargetTemplates)
            {
                foreach (var effect in tile.currentEffects)
                {
                    if (effect.originalTemplate == effectTemplate)
                    {
                        hasEffect = true;
                        break;
                    }   
                }

                if (hasEffect)
                {
                    break;
                }

            }

            if (!hasEffect)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        return false;
    }
}
