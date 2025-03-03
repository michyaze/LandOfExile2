using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetOtherUnitsWithEvolve : Ability
{
    public Ability otherAbility;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        foreach (Unit unit in MenuControl.Instance.battleMenu.GetAllUnitsOnBoard())
        {
            if (unit != null)
            {
                for (int ii = 0; ii < unit.GetComponentsInChildren<TriggerEvolve>().Length; ii += 1)
                {
                    
                    otherAbility.PerformAbility(sourceCard, unit.GetTile(), amount);
                    foreach (Trigger trigger in MenuControl.Instance.battleMenu.GetComponentsInChildren<Trigger>())
                    {
                        trigger.MinionEvolved((Minion)unit);
                    }
                }
            }

        }
    }

}
