using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackAndStun : Ability
{
    public Effect stunnedEffectTemplate;
    public int knockbackAmount = 1;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        int totalDamage = 0;

        Unit thisUnit = sourceCard.player.GetHero();
        if (sourceCard is Unit)
            thisUnit = (Unit)sourceCard;

        Unit targetUnit = targetTile.GetUnit();
        if (targetUnit == null) return;

        Unit otherUnit = null;

        if (!(targetUnit is LargeHero))
        {
            for (int ii = 1; ii <= 4; ii += 1)
            {
                if (targetTile == thisUnit.GetTile().GetTileLeft(ii))
                {
                    for (int xx = 1; xx <= knockbackAmount; xx += 1)
                    {
                        Tile tile = targetTile.GetTileLeft(xx);
                        if (tile != null && tile.isMoveable())
                        {
                            targetUnit.ForceMove(tile);
                        }
                        else
                        {
                            if (otherUnit == null && tile != null) otherUnit = tile.GetUnit();
                            totalDamage += knockbackAmount - (xx - 1);
                            break;
                        }
                    }
                    break;

                }
                else if (targetTile == thisUnit.GetTile().GetTileUp(ii))
                {
                    for (int xx = 1; xx <= knockbackAmount; xx += 1)
                    {
                        Tile tile = targetTile.GetTileUp(xx);
                        if (tile != null && tile.isMoveable())
                        {
                            targetUnit.ForceMove(tile);
                        }
                        else
                        {
                            if (otherUnit == null && tile != null) otherUnit = tile.GetUnit();
                            totalDamage += knockbackAmount - (xx - 1);
                            break;

                        }
                    }
                    break;
                }
                else if (targetTile == thisUnit.GetTile().GetTileRight(ii))
                {
                    for (int xx = 1; xx <= knockbackAmount; xx += 1)
                    {
                        Tile tile = targetTile.GetTileRight(xx);
                        if (tile != null && tile.isMoveable())
                        {
                            targetUnit.ForceMove(tile);
                        }
                        else
                        {
                            if (otherUnit == null && tile != null) otherUnit = tile.GetUnit();
                            totalDamage += knockbackAmount - (xx - 1);
                            break;

                        }
                    }
                    break;
                }
                else if (targetTile == thisUnit.GetTile().GetTileDown(ii))
                {
                    for (int xx = 1; xx <= knockbackAmount; xx += 1)
                    {
                        Tile tile = targetTile.GetTileDown(xx);
                        if (tile != null && tile.isMoveable())
                        {
                            targetUnit.ForceMove(tile);
                        }
                        else
                        {
                            if (otherUnit == null && tile != null) otherUnit = tile.GetUnit();
                            totalDamage += knockbackAmount - (xx - 1);
                            break;

                        }
                    }
                    break;
                }
            }

        }

        if (totalDamage > 0)
        {

            if (otherUnit != null)
            {
                targetUnit.ApplyEffect(sourceCard, this, stunnedEffectTemplate, 1);
                otherUnit.ApplyEffect(sourceCard, this, stunnedEffectTemplate, 1);
            }

            targetUnit.SufferDamage(sourceCard, this, totalDamage);
        }
    }

}
