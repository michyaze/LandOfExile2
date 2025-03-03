using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : Ability
{
    public Ability otherAbilityToPerform;
    public bool instantChain;

    public bool canHitEnemy;
    public bool canHitFriendlies;
    public bool canHitHeroes;
    public bool canHitMinions;
    public bool dontIncludeTargetUnit;
    public bool ignoreFirstDelay = true;
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
        return true;
    }

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        List<Unit> allUnitsHit = new List<Unit>();
        // if (sourceCard is Unit )
        // {
        //     allUnitsHit.Add(sourceCard as Unit);
        // }
        // if (targetTile.GetUnit())
        // {
        //     allUnitsHit.Add(targetTile.GetUnit());
        // }
        bool isTheFirst = true;
        for (int ii = 0; ii < 20; ii += 1)
        {
            List<Unit> unitsHitThisRound = new List<Unit>();
            List<Tile> tilesHitThisRound = new List<Tile>();

            if (allUnitsHit.Count == 0)
            {
                if (!dontIncludeTargetUnit && targetTile.GetUnit() != null)
                {
                    if (CanHitUnit(targetTile.GetUnit()))
                    {
                        unitsHitThisRound.Add(targetTile.GetUnit());
                        tilesHitThisRound.Add(targetTile);
                        allUnitsHit.Add(targetTile.GetUnit());
                    }
                }
                if (unitsHitThisRound.Count == 0)
                {
                    foreach (Tile tile in targetTile.GetAdjacentTilesLinear())
                    {
                        if (tile.GetUnit() != null && !allUnitsHit.Contains(tile.GetUnit()))
                        {

                            if (CanHitUnit(tile.GetUnit()))
                            {
                                unitsHitThisRound.Add(tile.GetUnit());
                                tilesHitThisRound.Add(tile);
                                allUnitsHit.Add(tile.GetUnit());
                            }

                        }
                    }
                }

            }
            else
            {
                foreach (Unit unit in allUnitsHit.ToArray())
                {
                    if (unit == null)
                    {
                        continue;
                    }
                    foreach (Tile tile in unit.GetAdjacentTiles())
                    {
                        if (tile.GetUnit() != null && !allUnitsHit.Contains(tile.GetUnit()))
                        {
                            if (!dontIncludeTargetUnit || tile.GetUnit() != targetTile.GetUnit())
                            {
                                if (CanHitUnit(tile.GetUnit()))
                                {
                                    unitsHitThisRound.Add(tile.GetUnit());
                                    allUnitsHit.Add(tile.GetUnit());
                                    tilesHitThisRound.Add(tile);
                                }
                            }
                        }
                    }
                }
            }

            if (unitsHitThisRound.Count == 0)
            {
                return;
            }
            else
            {
                
                if (instantChain || (isTheFirst && !ignoreFirstDelay))
                {
                    foreach (Unit unit in unitsHitThisRound)
                    {
                        otherAbilityToPerform.PerformAbility(sourceCard, unit.GetTile(), amount);
                    }
                }
                else
                {
                    MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                    {
                        foreach (Tile tile in tilesHitThisRound)
                        {
                                otherAbilityToPerform.PerformAbility(sourceCard, tile, amount);
                        }
                    }, true);

                }

                isTheFirst = false;
            }

        }
         
    }
}
