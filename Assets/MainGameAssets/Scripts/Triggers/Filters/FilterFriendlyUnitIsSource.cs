using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterFriendlyUnitIsSource : TriggerFilter
{
    public bool minionOnly;
    public bool heroOnly;
    public bool adjacentToThisUnit;
    public bool rangeAttackOnly;

    public List<Effect> effectTemplates;
    public override bool Check(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (heroOnly && !(sourceCard is Hero)) return false;

        if (minionOnly && !(sourceCard is Minion)) return false;

        if (rangeAttackOnly && (sourceCard is Unit) && !((Unit)sourceCard).isRangedAttack()) return false;

        if (adjacentToThisUnit && (GetCard().GetZone() != MenuControl.Instance.battleMenu.board || !((Unit)sourceCard).GetAdjacentTiles().Contains(((Unit)GetCard()).GetTile()))) return false;
        
        if (sourceCard is Unit unit)
        {
            if (effectTemplates!=null && effectTemplates.Count > 0)
            {
                bool hasEffect = false;
                foreach (var effectTemplate in effectTemplates)
                {
                    foreach (var effect in unit.currentEffects)
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
        }

        if (targetTile != null && sourceCard.player == GetCard().player) return true;
        
        

        return false;
    }
}
