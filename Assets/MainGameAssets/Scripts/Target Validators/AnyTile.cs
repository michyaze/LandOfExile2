using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnyTile", menuName = "Game Data/Target Type/AnyTile", order = 1)]
public class AnyTile : TargetValidator
{
    public bool noObstacle = false;
    public bool adjacentToMyHero;

    public override bool CanUnitTargetTile(Unit unit, Tile tile)
    {
        if (noObstacle)
        {
            if (tile.GetObstacle())
            {
                return false;
            }
        }
        
        if (adjacentToMyHero)
        {
            Hero hero = unit.player.GetHero();
            if (!hero.GetAdjacentTiles().Contains(tile))
            {
                return false;
            }
        }
        
        if (!hasEffect(tile))
        {
            return false;
        }

        if (hasEffectToNotTarget(tile))
        {
            return false;
        }

        return tile != null;
    }
}
