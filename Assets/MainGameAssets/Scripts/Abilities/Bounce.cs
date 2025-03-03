using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : Ability
{
    public Ability otherAbilityToPerform;
    //public bool instantChain;

    public int bounceTime = 3;

    public bool canHitEnemy;
    public bool canHitFriendlies;
    public bool canHitHeroes;
    public bool canHitMinions;
    public bool dontIncludeTargetUnit;
    public bool canBounceSelf = false;
    public bool performAtTheBeginning = false;
    public List<Effect> canHitWithHaveEffect = new List<Effect>();

    public virtual bool CanHitUnit(Unit unit)
    {
        if (!canHitEnemy && unit.player != GetCard().player)
        {
            return false;
        }

        if (!canHitFriendlies && unit.player == GetCard().player)
        {
            return false;
        }

        if (!canHitHeroes && unit is Hero)
        {
            return false;
        }

        if (!canHitMinions && unit is Minion)
        {
            return false;
        }

        if (canHitWithHaveEffect != null)
        {
            foreach (var effect2 in canHitWithHaveEffect)
            {
                if (effect2 == null)
                {
                    continue;
                }
                bool hasEffect = false;
                foreach (Effect effect in unit.currentEffects)
                {
                    if (effect.UniqueID == effect2.UniqueID)
                    {
                        hasEffect = true;
                        break;
                    }
                }

                if (!hasEffect)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private int currentBounceTime = 0;
    List<Unit> allUnitsHit = new List<Unit>();
    Tile lastTileHit = null;

    public void BounceAction(Card sourceCard, int amount = 0)
    {
        if (currentBounceTime >= bounceTime)
        {
            return;
        }

        List<Tile> tilesCanHitThisRound = new List<Tile>();

        if (lastTileHit.GetUnit())
        {
            
            foreach (Tile tile in lastTileHit.GetUnit().GetAdjacentTiles())
            {
                if (tile.GetUnit() != null /*&& !allUnitsHit.Contains(tile.GetUnit())*/)
                {
                    if (!canBounceSelf && tile == lastTileHit)
                    {
                        continue;
                    }

                    //if (!dontIncludeTargetUnit || tile.GetUnit() != targetTile.GetUnit())
                    {
                        if (CanHitUnit(tile.GetUnit()))
                        {
                            allUnitsHit.Add(tile.GetUnit());
                            tilesCanHitThisRound.Add(tile);
                        }
                    }
                }
            }
        }
        else
        {
            
            foreach (Tile tile in lastTileHit.GetAdjacentTilesLinear())
            {
                if (tile.GetUnit() != null /*&& !allUnitsHit.Contains(tile.GetUnit())*/)
                {
                    if (!canBounceSelf && tile == lastTileHit)
                    {
                        continue;
                    }

                    //if (!dontIncludeTargetUnit || tile.GetUnit() != targetTile.GetUnit())
                    {
                        if (CanHitUnit(tile.GetUnit()))
                        {
                            allUnitsHit.Add(tile.GetUnit());
                            tilesCanHitThisRound.Add(tile);
                        }
                    }
                }
            }
        }

        if (tilesCanHitThisRound.Count > 0)
        {
            lastTileHit = tilesCanHitThisRound.PickItem();
        }
        else
        {
            return;
        }

        MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(),
            () =>
            {
                otherAbilityToPerform.PerformAbility(sourceCard, lastTileHit, amount);

                currentBounceTime++;
                BounceAction(sourceCard, amount);
            }, true);
    }

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        lastTileHit = null;
        currentBounceTime = 0;
        allUnitsHit.Clear();

        if (performAtTheBeginning)
        {
            otherAbilityToPerform.PerformAbility(sourceCard, targetTile, amount);
        }

        List<Tile> tilesCanHitThisRound = new List<Tile>();

        if (allUnitsHit.Count == 0)//这个不是永远true
        {
            if (!dontIncludeTargetUnit && targetTile.GetUnit() != null)
            {
                if (CanHitUnit(targetTile.GetUnit()))
                {
                    tilesCanHitThisRound.Add(targetTile);
                    allUnitsHit.Add(targetTile.GetUnit());
                }
            }

            if (lastTileHit == null)
            {
                if (targetTile.GetUnit())
                {
                    foreach (Tile tile in targetTile.GetUnit().GetAdjacentTiles())
                    {
                        if (tile.GetUnit() != null /* && !allUnitsHit.Contains(tile.GetUnit())*/)
                        {
                            if (!canBounceSelf && tile == lastTileHit)
                            {
                                continue;
                            }

                            if (CanHitUnit(tile.GetUnit()))
                            {
                                tilesCanHitThisRound.Add(tile);
                                allUnitsHit.Add(tile.GetUnit());
                            }
                        }
                    }
                }
                else
                {
                    foreach (Tile tile in targetTile.GetAdjacentTilesLinear())
                    {
                        if (tile.GetUnit() != null /* && !allUnitsHit.Contains(tile.GetUnit())*/)
                        {
                            if (!canBounceSelf && tile == lastTileHit)
                            {
                                continue;
                            }

                            if (CanHitUnit(tile.GetUnit()))
                            {
                                tilesCanHitThisRound.Add(tile);
                                allUnitsHit.Add(tile.GetUnit());
                            }
                        }
                    }
                }
            }


            if (tilesCanHitThisRound.Count > 0)
            {
                lastTileHit = tilesCanHitThisRound.PickItem();
            }
            else
            {
                return;
            }

            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(),
                () =>
                {
                    otherAbilityToPerform.PerformAbility(sourceCard, lastTileHit, amount);

                    currentBounceTime++;
                    BounceAction(sourceCard, amount);
                }, true);
        }
    }
    // public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    // {
    //
    //     List<Unit> allUnitsHit = new List<Unit>();
    //     Tile lastTileHit = null;
    //     // if (sourceCard is Unit )
    //     // {
    //     //     allUnitsHit.Add(sourceCard as Unit);
    //     // }
    //     // if (targetTile.GetUnit())
    //     // {
    //     //     allUnitsHit.Add(targetTile.GetUnit());
    //     // }
    //
    //     for (int ii = 0; ii < bounceTime; ii += 1)
    //     {
    //         List<Tile> tilesCanHitThisRound = new List<Tile>();
    //
    //         if (allUnitsHit.Count == 0)
    //         {
    //             if (!dontIncludeTargetUnit && targetTile.GetUnit() != null)
    //             {
    //                 if (CanHitUnit(targetTile.GetUnit()))
    //                 {
    //                     tilesCanHitThisRound.Add(targetTile);
    //                     allUnitsHit.Add(targetTile.GetUnit());
    //                     lastTileHit = targetTile;
    //                 }
    //             }
    //             if (lastTileHit == null)
    //             {
    //                 foreach (Tile tile in targetTile.GetAdjacentTilesLinear())
    //                 {
    //                     if (tile.GetUnit() != null/* && !allUnitsHit.Contains(tile.GetUnit())*/)
    //                     {
    //                         if (!canBounceSelf && tile == lastTileHit)
    //                         {
    //                             continue;
    //                         }
    //                         if (CanHitUnit(tile.GetUnit()))
    //                         {
    //                             tilesCanHitThisRound.Add(tile);
    //                             allUnitsHit.Add(tile.GetUnit());
    //                         }
    //
    //                     }
    //                 }
    //
    //             }
    //
    //         }
    //         else
    //         {
    //             foreach (Tile tile in lastTileHit.GetAdjacentTilesLinear())
    //             {
    //                 if (tile.GetUnit() != null /*&& !allUnitsHit.Contains(tile.GetUnit())*/)
    //                 {
    //                     
    //                     if (!canBounceSelf && tile == lastTileHit)
    //                     {
    //                         continue;
    //                     }
    //                     //if (!dontIncludeTargetUnit || tile.GetUnit() != targetTile.GetUnit())
    //                     {
    //                         if (CanHitUnit(tile.GetUnit()))
    //                         {
    //                             allUnitsHit.Add(tile.GetUnit());
    //                             tilesCanHitThisRound.Add(tile);
    //                         }
    //                     }
    //                 }
    //             }
    //         }
    //
    //         if (tilesCanHitThisRound.Count > 0)
    //         {
    //                     
    //             lastTileHit = tilesCanHitThisRound.PickItem();
    //         }
    //         else
    //         {
    //             return;
    //         }
    //         
    //         {
    //             
    //             // if (instantChain)
    //             // {
    //             //     foreach (Unit unit in unitsHitThisRound)
    //             //     {
    //             //         otherAbilityToPerform.PerformAbility(sourceCard, unit.GetTile(), amount);
    //             //     }
    //             // }
    //             // else
    //             {
    //                 MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
    //                 {
    //                             otherAbilityToPerform.PerformAbility(sourceCard, lastTileHit, amount);
    //                     
    //                 }, true);
    //
    //             }
    //         }
    //
    //     }
    //      
    // }
}