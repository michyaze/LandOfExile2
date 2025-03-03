using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMovementToLeap : Ability
{
    public List<MovementLinear> movementLinear;
    public List<MovementLinear> movementLeap;
    public bool ignoreLargeHero;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        var unit = targetTile.GetUnit();
        if (ignoreLargeHero && unit is LargeHero)
        {
            return;
        }
        
        if(!unit){return;}
        for (int i = 0; i < movementLinear.Count; i++)
        {
            if(movementLinear[i] == unit.movementType){
                unit.ChangeMovementTemporarily((movementLeap[i]));
                break;
            }
        }
    }
}
