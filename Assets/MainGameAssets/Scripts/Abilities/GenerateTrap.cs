using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTrap : Ability
{
    public WeatherTrap trapPrefab;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (targetTile.GetObstacle() != null)
        {
            return;
        }

        if (targetTile.GetTrap() != null)
        {
            var oldTrap = targetTile.GetTrap();
            if (oldTrap.Persistent)
            {
                return;
            }
            MenuControl.Instance.battleMenu.FullyRemoveTrap(oldTrap);
        }

        //todo if already has trap, remove it
        // if has unit on trap, trigger move into
        var trap = Instantiate(trapPrefab, targetTile.transform.position, Quaternion.identity,
            MenuControl.Instance.battleMenu.trapParent);
        trap.transform.position = targetTile.transform.position;
        MenuControl.Instance.battleMenu.boardMenu.AddTrapToTile(trap.GetComponent<WeatherTrap>(), targetTile);


        
    }
}