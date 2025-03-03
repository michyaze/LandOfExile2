using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnyTeamUnitAdjacentToMyHero", menuName = "Game Data/Target Type/Any Enemy Unit Next To My Hero", order = 1)]
public class AnyTeamUnitAdjacentToMyHero : TargetValidator
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
            Hero hero = unit.player.GetHero();
            if (hero.GetAdjacentTiles().Contains(tile) != adjacent)
            {
                return false;
            }

            if (effectTemplates!=null && effectTemplates.Count > 0)
            {
                bool hasEffect = false;
                foreach (var effectTemplate in effectTemplates)
                {
                    foreach (var effect in otherUnit.currentEffects)
                    {
                        if (effect.originalTemplate == effectTemplate)
                        {
                            hasEffect = true;
                            break;
                        }   
                    }

                    if (hasEffect)
                    {
                        break;
                    }

                }

                if (!hasEffect)
                {
                    return false;
                }
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
