using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnyTeamUnit", menuName = "Game Data/Target Type/Any Enemy Unit", order = 1)]
public class AnyTeamUnit : TargetValidator
{
    public bool myTeam;
    public bool enemyTeam;
    public bool includeMinions = true;
    public bool includeHeroes = true;
    public bool nonLargeHeroes;
    public bool notMyHero;

    public override bool CanUnitTargetTile(Unit unit, Tile tile)
    {
        if (tile == null)
        {
            return false;
        }
        //Check tile is unfilled
        Unit otherUnit = tile.GetUnit();
        if (otherUnit != null)
        {
            
            if (!hasEffect(otherUnit))
            {
                return false;
            }

            if (hasEffectToNotTarget(otherUnit))
            {
                return false;
            }
            if (!includeMinions && (otherUnit is Minion)) return false;
            if (!includeHeroes && (otherUnit is Hero)) return false;

            if (nonLargeHeroes && (otherUnit is LargeHero)) return false;
            if (notMyHero && otherUnit == unit.player.GetHero()) return false; 

            if (enemyTeam && otherUnit.player != unit.player) return true;
            if (myTeam && otherUnit.player == unit.player) return true;
        }

        return false;
    }
}
