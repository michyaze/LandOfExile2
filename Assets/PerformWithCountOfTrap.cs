using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformWithCountOfTrap : Ability
{
    public List<WeatherTrap> traps;
    public Ability anotherAbility;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        int res = 0;
        foreach (var trap in MenuControl.Instance.battleMenu.boardMenu.traps)
        {
            if (trap == null)
            {
                continue;
            }
            foreach (var target in traps)
            {

                if (trap.UniqueID == target.UniqueID)
                {
                    res += 1;
                    break;
                }
            }
        }

        if (res > 0)
        {
            anotherAbility.PerformAbility(sourceCard, targetTile, res);
        }
    }
}
