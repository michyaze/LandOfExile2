using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTrapAmount : Trigger
{
    public int targetAmount;
    public List<WeatherTrap> trapTypes;
    public Ability anotherAbility;

    public override void TrapGenerated(Tile tile, WeatherTrap trap)
    {
        int amount = 0;
        foreach (var tr in MenuControl.Instance.battleMenu.boardMenu.traps)
        {
            if (tr == null)
            {
                continue;
            }
            foreach (var type in trapTypes)
            {

                if (tr.UniqueID == type.UniqueID)
                {
                    amount++;
                    break;
                }
            }
        }

        if (amount >= targetAmount)
        {
            anotherAbility.PerformAbility(GetCard(),(GetCard() as Unit) .GetTile(),amount);
        }
        
    }
}
