using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessMultipleAbilitiesBelow : Ability
{
    public int abilitiesToCall = 2;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        bool startCounting = false;
        int countDown = abilitiesToCall;
        foreach (Ability ability in GetComponents<Ability>())
        {
            if (ability == this)
            {
                startCounting = true;
            }
            else
            {
                if (startCounting)
                {
                    countDown -= 1;

                    ability.PerformAbility(sourceCard, targetTile, amount);

                    if (countDown == 0) break;
                }
            }

        }
    }

  
}
