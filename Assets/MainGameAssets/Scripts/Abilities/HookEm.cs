using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookEm : Ability
{
    public Effect stunEffectTemplate;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if(this.GetTargetValidator() && !CanTargetTile(sourceCard, targetTile))
        {
            return;
        }
        List<Tile> empytAdjacentTiles = new List<Tile>();
        foreach  (Tile tile in sourceCard.player.GetHero().GetAdjacentTiles())
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
