using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWasntMe : Trigger
{
    public bool canTrigger = true;

    public override void UnitDamaged(Card sourceCard, Unit unit, Ability ability, int damageAmount,
        bool triggerUnitDamage = true)
    {
        if (unit == GetCard() && unit.GetHP() <= 0 && unit.GetHP() + damageAmount > 0)
        {
            if (unit.player.GetMinionsOnBoard().Count > 0)
            {
                if (canTrigger)
                {
                    canTrigger = false;
                    Minion minion = unit.player.GetMinionsOnBoard()[Random.Range(0, unit.player.GetMinionsOnBoard().Count)];

                    Tile tile1 = unit.GetTile();

                    Tile tile2 = minion.GetTile();

                    unit.ChangePosition(minion);
                    
                    unit.ChangeCurrentHP(this, unit.GetHP() + damageAmount);
                    minion.SufferDamage(GetCard(), this, damageAmount);
                }
            }
        }
    }
}
