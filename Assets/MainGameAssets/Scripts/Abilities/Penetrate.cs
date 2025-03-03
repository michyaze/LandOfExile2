using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penetrate : Ability
{
    public int overrideRange;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        Unit thisUnit = (Unit)sourceCard;

        int range = overrideRange;

        if (range == 0 && GetComponentInParent<Attack>() != null && GetComponentInParent<Attack>().GetTargetValidator() is TargetLinear)
        {
            range = ((TargetLinear)GetComponent<Attack>().GetTargetValidator()).range;
        }

        if (range > 0)
        {
            for (int ii = 1; ii <= 4; ii += 1)
            {
                if (targetTile == thisUnit.GetTile().GetTileLeft(ii))
                {
                    for (int xx = 1; xx <= range; xx += 1)
                    {
                        Tile tile = thisUnit.GetTile().GetTileLeft(xx);
                        if (tile != null && tile.GetUnit() != null && tile.GetUnit().player != sourceCard.player && tile != targetTile)
                        {
                            tile.GetUnit().SufferDamage(sourceCard, this, ((Unit)sourceCard).GetPower((Unit)sourceCard, targetTile.GetUnit()));
                        }
                    }
                    break;

                }
                else if (targetTile == thisUnit.GetTile().GetTileUp(ii))
                {
                    for (int xx = 1; xx <= range; xx += 1)
                    {
                        Tile tile = thisUnit.GetTile().GetTileUp(xx);
                        if (tile != null && tile.GetUnit() != null && tile.GetUnit().player != sourceCard.player && tile != targetTile)
                        {
                            tile.GetUnit().SufferDamage(sourceCard, this, ((Unit)sourceCard).GetPower((Unit)sourceCard, targetTile.GetUnit()));
                        }
                    }
                    break;
                }
                else if (targetTile == thisUnit.GetTile().GetTileRight(ii))
                {
                    for (int xx = 1; xx <= range; xx += 1)
                    {
                        Tile tile = thisUnit.GetTile().GetTileRight(xx);
                        if (tile != null && tile.GetUnit() != null && tile.GetUnit().player != sourceCard.player && tile != targetTile)
                        {
                            tile.GetUnit().SufferDamage(sourceCard, this, ((Unit)sourceCard).GetPower((Unit)sourceCard, targetTile.GetUnit()));
                        }
                    }
                    break;
                }
                else if (targetTile == thisUnit.GetTile().GetTileDown(ii))
                {
                    for (int xx = 1; xx <= range; xx += 1)
                    {
                        Tile tile = thisUnit.GetTile().GetTileDown(xx);
                        if (tile != null && tile.GetUnit() != null && tile.GetUnit().player != sourceCard.player && tile != targetTile)
                        {
                            tile.GetUnit().SufferDamage(sourceCard, this, ((Unit)sourceCard).GetPower((Unit)sourceCard, targetTile.GetUnit()));
                        }
                    }
                    break;
                }
            }
        }

    }

}
