using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TargetLinear", menuName = "Game Data/Target Type/Linear", order = 1)]
public class TargetLinear : TargetValidator
{
    public bool myTeam;
    public bool allUnits;
    public bool allNonLargeUnits;
    public int range;
    public bool allowDiagonal;
    public bool notMyHero;
    public bool notAdjacent;
    public bool andMyHeroFromAnyTile;

    
    public override bool CanUnitTargetTile(Unit unit, Tile tile)
    {
       
        var targetUnit = tile.GetUnit();
        //Check tile is filled
        if (targetUnit != null)
        {
            if (unit != null)
            {
                var unitPreventions = unit.GetComponentsInChildren<AttackRangeTargetPrevention>();
                if (unitPreventions == null || unitPreventions.Length == 0)
                {
                    //todo 这个检查有点hack了，因为现在只有飞行一种targetPrevention，且飞行可以打飞行。所以如果unit有飞行就不检查被攻击者的飞行状态了
                    var targetPreventions = targetUnit.GetComponentsInChildren<AttackRangeTargetPrevention>();
                    if (targetPreventions.Length > 0 )
                    {
                        foreach (var targetPrevention in targetPreventions)
                        {
                            if (targetPrevention.preventTargetValidation.Contains(this))
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            
            
            if (notMyHero && tile.GetUnit() == unit.player.GetHero()) return false;

            if (andMyHeroFromAnyTile && tile.GetUnit() == unit.player.GetHero()) return true;

            if (unit is LargeHero)
            {

                // if (((LargeHero)unit).GetTiles().Contains(tile))
                // {
                //     return false;
                // }
                foreach (Tile largeHeroTile in ((LargeHero)unit).GetTiles())
                {
                    if (largeHeroTile.GetAdjacentTilesLinear(range).Contains(tile))
                    {

                        if (allUnits) return true;
                        if (!myTeam && tile.GetUnit().player != unit.player)
                            return true;
                        if (myTeam && tile.GetUnit().player == unit.player)
                            return true;

                    }

                    if (allowDiagonal)
                    {
                        if (largeHeroTile.GetTileUp() != null)
                        {
                            if (largeHeroTile.GetTileUp().GetTileLeft() == tile)
                            {
                                if (largeHeroTile.GetTileUp().GetTileLeft().GetUnit() != null)
                                {
                                    Tile finalTile = largeHeroTile.GetTileUp().GetTileLeft();
                                    if (allUnits) return true;
                                    if (!myTeam && finalTile.GetUnit().player != unit.player)
                                        return true;
                                    if (myTeam && finalTile.GetUnit().player == unit.player)
                                        return true;
                                }
                            }
                            if (largeHeroTile.GetTileUp().GetTileRight() == tile)
                            {
                                if (largeHeroTile.GetTileUp().GetTileRight().GetUnit() != null)
                                {
                                    Tile finalTile = largeHeroTile.GetTileUp().GetTileRight();
                                    if (allUnits) return true;
                                    if (!myTeam && finalTile.GetUnit().player != unit.player)
                                        return true;
                                    if (myTeam && finalTile.GetUnit().player == unit.player)
                                        return true;
                                }
                            }

                        }
                        if (largeHeroTile.GetTileDown() != null)
                        {
                            if (largeHeroTile.GetTileDown().GetTileLeft() == tile)
                            {
                                if (largeHeroTile.GetTileDown().GetTileLeft().GetUnit() != null)
                                {
                                    Tile finalTile = largeHeroTile.GetTileDown().GetTileLeft();
                                    if (allUnits) return true;
                                    if (!myTeam && finalTile.GetUnit().player != unit.player)
                                        return true;
                                    if (myTeam && finalTile.GetUnit().player == unit.player)
                                        return true;
                                }
                            }
                            if (largeHeroTile.GetTileDown().GetTileRight() == tile)
                            {
                                if (largeHeroTile.GetTileDown().GetTileRight().GetUnit() != null)
                                {
                                    Tile finalTile = largeHeroTile.GetTileDown().GetTileRight();
                                    if (allUnits) return true;
                                    if (!myTeam && finalTile.GetUnit().player != unit.player)
                                        return true;
                                    if (myTeam && finalTile.GetUnit().player == unit.player)
                                        return true;
                                }
                            }

                        }
                        if (largeHeroTile.GetTileRight() != null)
                        {
                            if (largeHeroTile.GetTileRight().GetTileUp() == tile)
                            {
                                if (largeHeroTile.GetTileRight().GetTileUp().GetUnit() != null)
                                {
                                    Tile finalTile = largeHeroTile.GetTileRight().GetTileUp();
                                    if (allUnits) return true;
                                    if (!myTeam && finalTile.GetUnit().player != unit.player)
                                        return true;
                                    if (myTeam && finalTile.GetUnit().player == unit.player)
                                        return true;
                                }
                            }
                            if (largeHeroTile.GetTileRight().GetTileDown() == tile)
                            {
                                if (largeHeroTile.GetTileRight().GetTileDown().GetUnit() != null)
                                {
                                    Tile finalTile = largeHeroTile.GetTileRight().GetTileDown();
                                    if (allUnits) return true;
                                    if (!myTeam && finalTile.GetUnit().player != unit.player)
                                        return true;
                                    if (myTeam && finalTile.GetUnit().player == unit.player)
                                        return true;
                                }
                            }

                        }
                        if (largeHeroTile.GetTileLeft() != null)
                        {
                            if (largeHeroTile.GetTileLeft().GetTileUp() == tile)
                            {
                                if (largeHeroTile.GetTileLeft().GetTileUp().GetUnit() != null)
                                {
                                    Tile finalTile = largeHeroTile.GetTileLeft().GetTileUp();
                                    if (allUnits) return true;
                                    if (!myTeam && finalTile.GetUnit().player != unit.player)
                                        return true;
                                    if (myTeam && finalTile.GetUnit().player == unit.player)
                                        return true;
                                }
                            }
                            if (largeHeroTile.GetTileLeft().GetTileDown() == tile)
                            {
                                if (largeHeroTile.GetTileLeft().GetTileDown().GetUnit() != null)
                                {
                                    Tile finalTile = largeHeroTile.GetTileLeft().GetTileDown();
                                    if (allUnits) return true;
                                    if (!myTeam && finalTile.GetUnit().player != unit.player)
                                        return true;
                                    if (myTeam && finalTile.GetUnit().player == unit.player)
                                        return true;
                                }
                            }

                        }
                    }
                }
            }
            else
            {
                var shieldEffectToForceAttackAdjacent = false;
                var shields  = unit.GetEffectsOfType<ShieldEffect>();
                foreach (var shield in shields)
                {
                    if (shield is ShieldEffect se && se.canAttackAdjacent)
                    {
                        shieldEffectToForceAttackAdjacent = true;
                        break;
                    }
                }
                if (unit.GetTile().GetAdjacentTilesLinear(range).Contains(tile) && (!notAdjacent || shieldEffectToForceAttackAdjacent || (notAdjacent && !unit.GetTile().GetAdjacentTilesLinear(1).Contains(tile))))
                {
                    if (allUnits || allNonLargeUnits) return true;
                    if (!myTeam && tile.GetUnit().player != unit.player)
                        return true;
                    if (myTeam && tile.GetUnit().player == unit.player)
                        return true;
                }

                if (allowDiagonal)
                {
                    Tile unitTile = unit.GetTile();
                    if (unitTile.GetTileUp() != null)
                    {
                        if (unitTile.GetTileUp().GetTileLeft() == tile)
                        {
                            if (unitTile.GetTileUp().GetTileLeft().GetUnit() != null)
                            {
                                Tile finalTile = unitTile.GetTileUp().GetTileLeft();
                                if (allUnits || allNonLargeUnits) return true;
                                if (!myTeam && finalTile.GetUnit().player != unit.player)
                                    return true;
                                if (myTeam && finalTile.GetUnit().player == unit.player)
                                    return true;
                            }
                        }
                        if (unitTile.GetTileUp().GetTileRight() == tile)
                        {
                            if (unitTile.GetTileUp().GetTileRight().GetUnit() != null)
                            {
                                Tile finalTile = unitTile.GetTileUp().GetTileRight();
                                if (allUnits) return true;
                                if (!myTeam && finalTile.GetUnit().player != unit.player)
                                    return true;
                                if (myTeam && finalTile.GetUnit().player == unit.player)
                                    return true;
                            }
                        }

                    }
                    if (unitTile.GetTileDown() != null)
                    {
                        if (unitTile.GetTileDown().GetTileLeft() == tile)
                        {
                            if (unitTile.GetTileDown().GetTileLeft().GetUnit() != null)
                            {
                                Tile finalTile = unitTile.GetTileDown().GetTileLeft();
                                if (allUnits || allNonLargeUnits) return true;
                                if (!myTeam && finalTile.GetUnit().player != unit.player)
                                    return true;
                                if (myTeam && finalTile.GetUnit().player == unit.player)
                                    return true;
                            }
                        }
                        if (unitTile.GetTileDown().GetTileRight() == tile)
                        {
                            if (unitTile.GetTileDown().GetTileRight().GetUnit() != null)
                            {
                                Tile finalTile = unitTile.GetTileDown().GetTileRight();
                                if (allUnits || allNonLargeUnits) return true;
                                if (!myTeam && finalTile.GetUnit().player != unit.player)
                                    return true;
                                if (myTeam && finalTile.GetUnit().player == unit.player)
                                    return true;
                            }
                        }

                    }
                    if (unitTile.GetTileRight() != null)
                    {
                        if (unitTile.GetTileRight().GetTileUp() == tile)
                        {
                            if (unitTile.GetTileRight().GetTileUp().GetUnit() != null)
                            {
                                Tile finalTile = unitTile.GetTileRight().GetTileUp();
                                if (allUnits || allNonLargeUnits) return true;
                                if (!myTeam && finalTile.GetUnit().player != unit.player)
                                    return true;
                                if (myTeam && finalTile.GetUnit().player == unit.player)
                                    return true;
                            }
                        }
                        if (unitTile.GetTileRight().GetTileDown() == tile)
                        {
                            if (unitTile.GetTileRight().GetTileDown().GetUnit() != null)
                            {
                                Tile finalTile = unitTile.GetTileRight().GetTileDown();
                                if (allUnits || allNonLargeUnits) return true;
                                if (!myTeam && finalTile.GetUnit().player != unit.player)
                                    return true;
                                if (myTeam && finalTile.GetUnit().player == unit.player)
                                    return true;
                            }
                        }

                    }
                    if (unitTile.GetTileLeft() != null)
                    {
                        if (unitTile.GetTileLeft().GetTileUp() == tile)
                        {
                            if (unitTile.GetTileLeft().GetTileUp().GetUnit() != null)
                            {
                                Tile finalTile = unitTile.GetTileLeft().GetTileUp();
                                if (allUnits || allNonLargeUnits) return true;
                                if (!myTeam && finalTile.GetUnit().player != unit.player)
                                    return true;
                                if (myTeam && finalTile.GetUnit().player == unit.player)
                                    return true;
                            }
                        }
                        if (unitTile.GetTileLeft().GetTileDown() == tile)
                        {
                            if (unitTile.GetTileLeft().GetTileDown().GetUnit() != null)
                            {
                                Tile finalTile = unitTile.GetTileLeft().GetTileDown();
                                if (allUnits || allNonLargeUnits) return true;
                                if (!myTeam && finalTile.GetUnit().player != unit.player)
                                    return true;
                                if (myTeam && finalTile.GetUnit().player == unit.player)
                                    return true;
                            }
                        }

                    }
                }
            }

        }



        return false;
    }
}
