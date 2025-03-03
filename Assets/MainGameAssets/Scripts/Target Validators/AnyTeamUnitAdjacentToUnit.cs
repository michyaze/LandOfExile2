using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnyTeamUnitAdjacentToUnit", menuName = "Game Data/Target Type/Any Enemy Unit Next To Unit", order = 1)]
public class AnyTeamUnitAdjacentToUnit : TargetValidator
{
    public bool myTeam;
    public bool enemyTeam;
    public bool includeMinions = true;
    public bool includeHeroes = true;
    public bool notLargeHeroes = false;
    public bool adjacent = true;


    public override bool CanUnitTargetTile(Unit unit, Tile tile)
    {
        //Check tile is unfilled
        Unit otherUnit = tile.GetUnit();
        if (otherUnit != null)
        {
            if (unit.GetAdjacentTiles().Contains(tile) != adjacent)
            {
                return false;
            }

            if (!hasEffect(otherUnit))
            {
                return false;
            }
            

            if (!includeMinions && (otherUnit is Minion)) return false;
            if (!includeHeroes && (otherUnit is Hero)) return false;
            if (notLargeHeroes && (otherUnit is LargeHero)) return false;

            if (enemyTeam && otherUnit.player != unit.player) return true;
            if (myTeam && otherUnit.player == unit.player) return true;

        }

        return false;
    }
}
