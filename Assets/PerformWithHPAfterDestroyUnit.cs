using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformWithHPAfterDestroyUnit : Ability
{
    //记录挡墙血量
    //移除HeroDestroy trigger
    //干掉Unit
    //释放另一个Ability

    private int hpBeforeDestroy;
    public bool removeHeroDestroyTrigger = true;
    public Ability anotherAbility;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        var unit = GetCard() as Unit;
        if (unit)
        {
            
            if (removeHeroDestroyTrigger)
            {
                foreach (var trigger in GetComponents<TriggerMultiUnitOnBoard>())
                {
                    if (trigger.triggerType == TriggerType.HeroDestroyed)
                    {
                        trigger.enabled = false;
                    }
                }
            }

            hpBeforeDestroy = unit.GetHP() ;
            unit.SufferDamage(sourceCard, this, 0, true);
            anotherAbility.PerformAbility(sourceCard, targetTile, hpBeforeDestroy);
        }
    }
}
