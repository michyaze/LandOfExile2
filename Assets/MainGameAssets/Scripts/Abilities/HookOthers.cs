using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookOthers : Ability
{
    public Effect stunEffectTemplate;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        var sourceUnit = sourceCard as Unit;
        if (sourceUnit == null)
        {
            sourceUnit = sourceCard.player.GetHero();
        }
        List<Tile> empytAdjacentTiles = new List<Tile>();
        //if (sourceCard is Unit unit)
        {
            foreach  (Tile tile in sourceUnit.GetTile().GetAdjacentTilesLinear(1))
            {
                if (tile.isMoveable())
                {
                    empytAdjacentTiles.Add(tile);
                }
            }

            if (empytAdjacentTiles.Count > 0)
            {
                targetTile.GetUnit().ForceMove(empytAdjacentTiles[Random.Range(0, empytAdjacentTiles.Count)]);
            }
            else
            {
                if (stunEffectTemplate)
                {
                    targetTile.GetUnit().ApplyEffect(sourceCard, this, stunEffectTemplate, 1);
                }
            }
        }
        
    }
}
