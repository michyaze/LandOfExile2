using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainHaste : Ability
{

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        Unit unit = targetTile.GetUnit();

        if (unit.GetEffectsWithTemplate(MenuControl.Instance.battleMenu.summoningSickEffectTemplate).Count > 0)
        {
            unit.RemoveEffect(null, null, unit.GetEffectsWithTemplate(MenuControl.Instance.battleMenu.summoningSickEffectTemplate)[0]);
            unit.remainingMoves = unit.GetInitialMoves(); 
            unit.remainingActions = unit.GetInitialActions(); //TODO this is because Dash/Multistrike applied after summoning so its may be missing some (fix later)
        }
        else
        {
            int moves = unit.GetInitialMoves();
            int actions = unit.GetInitialActions();
            unit.remainingMoves = moves - (moves - 1 - unit.remainingMoves); 
            unit.remainingActions = actions - (actions - unit.remainingActions); //TODO this is because Dash/Multistrike applied after summoning so its may be missing some (fix later)
        }
    }
}
