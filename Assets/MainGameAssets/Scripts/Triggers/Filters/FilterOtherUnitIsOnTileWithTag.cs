using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterOtherUnitIsOnTileWithTag : TriggerFilter
{
    public bool enemyUnits;
    public bool friendlyUnits;
    public CardTag cardTag;

    public override bool Check(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (targetTile != null) {
            if ((enemyUnits && targetTile.GetUnit().player == sourceCard.player.GetOpponent()) || (friendlyUnits && targetTile.GetUnit().player == sourceCard.player))
            {
                if (cardTag == null || targetTile.GetUnit().cardTags.Contains(cardTag))
                {
                    if (targetTile.GetUnit() != sourceCard) return true;
                }
            }
        }

        return false;
    }
}
