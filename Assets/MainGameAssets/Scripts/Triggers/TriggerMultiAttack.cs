using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMultiAttack : Trigger
{
    [HideInInspector]
    public bool hasTriggered;

    public int attackCount = 1;

    public override void UnitAttacks(Unit attacker, Unit defender, Tile targetTile, bool initialAttack)
    {
        if (CanTargetTile(attacker, targetTile))
        {
            
            if (initialAttack && attacker == GetCard() && !hasTriggered)
            {
                Unit thisCard = attacker;
                Unit defendingCard = defender;
                hasTriggered = true;
                for (int i = 0; i < attackCount; i++)
                {
                    
                    MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                    {
                        if (defendingCard.GetTile() != null)
                        {
                            thisCard.ForceAttack(defendingCard.GetTile());
                        }
                        hasTriggered = false;
                    });
                }
            }
        }
    }

}
