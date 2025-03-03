using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOnMinionWillBeDestroyedKnockbackAdjacentUnits : Trigger
{

    public int knockbackAmount = 1;

    public override void MinionDestroyed(Card sourceCard, Ability ability, int damageAmount, Minion minion)
    {
        Unit unit = (Unit)GetCard();

        if (minion == unit)
        {
            Zone zone = minion.GetZone();
            if (minion.GetZone() != MenuControl.Instance.battleMenu.board) return;

            Tile originalTile = unit.GetTile();
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                foreach (Tile targetTile in originalTile.GetAdjacentTilesLinear(1))
                {

                    Unit targetUnit = targetTile.GetUnit();
                    if (!(targetUnit is LargeHero) && targetUnit != null)
                    {

                        int totalDamage = 0;

                        for (int ii = 1; ii <= 4; ii += 1)
                        {
                            if (targetTile == originalTile.GetTileLeft(ii))
                            {
                                for (int xx = 1; xx <= knockbackAmount; xx += 1)
                                {
                                    Tile tile = targetTile.GetTileLeft(xx);
                                    if (tile != null && tile.isMoveable())
                                    {
                                        targetUnit.MoveToTile(tile);
                                    }
                                    else
                                    {
                                        totalDamage += knockbackAmount - (xx - 1);
                                        break;
                                    }
                                }
                                break;

                            }
                            else if (targetTile == originalTile.GetTileUp(ii))
                            {
                                for (int xx = 1; xx <= knockbackAmount; xx += 1)
                                {
                                    Tile tile = targetTile.GetTileUp(xx);
                                    if (tile != null && tile.isMoveable())
                                    {
                                        targetUnit.MoveToTile(tile);
                                    }
                                    else
                                    {
                                        totalDamage += knockbackAmount - (xx - 1);
                                        break;
                                    }
                                }
                                break;
                            }
                            else if (targetTile == originalTile.GetTileRight(ii))
                            {
                                for (int xx = 1; xx <= knockbackAmount; xx += 1)
                                {
                                    Tile tile = targetTile.GetTileRight(xx);
                                    if (tile != null && tile.isMoveable())
                                    {
                                        targetUnit.MoveToTile(tile);
                                    }
                                    else
                                    {
                                        totalDamage += knockbackAmount - (xx - 1);
                                        break;
                                    }
                                }
                                break;
                            }
                            else if (targetTile == originalTile.GetTileDown(ii))
                            {
                                for (int xx = 1; xx <= knockbackAmount; xx += 1)
                                {
                                    Tile tile = targetTile.GetTileDown(xx);
                                    if (tile != null && tile.isMoveable())
                                    {
                                        targetUnit.MoveToTile(tile);
                                    }
                                    else
                                    {
                                        totalDamage += knockbackAmount - (xx - 1);
                                        break;
                                    }
                                }
                                break;
                            }
                        }



                        if (totalDamage > 0)
                            targetUnit.SufferDamage(GetCard(), this, totalDamage);

                    }
                }
            });
        }

    }
}
