using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrap : Ability
{
    public Effect templateEffect;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (sourceCard is WeatherTrap trap)
        {
            var unit = trap.GetTile().GetUnit();
            var succeed = MenuControl.Instance.battleMenu.MoveTrapToRandomTile(trap);
            // if (succeed)
            // {
            //     if (unit)
            //     {
            //         foreach (Effect effect in targetTile.GetUnit().GetEffectsWithTemplate(templateEffect).ToArray())
            //         {
            //             unit.RemoveEffect(sourceCard, this, effect);
            //         }
            //     }
            // }
        }
    }
}