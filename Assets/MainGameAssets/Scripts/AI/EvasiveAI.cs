using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "EvasiveAI", menuName = "Game Data/AI/EvasiveAI", order = 1)]
public class EvasiveAI : AIControl
{
    public override bool TakeAITurn(Player player)
    {

        //Hero hits the enemies in range //without retaliate
        foreach (Unit enemyUnit in player.GetOpponent().cardsOnBoard)
        {
            Hero unit = player.GetHero();
            if (unit.GetPower() > 0)
            {
                if (unit.CanTarget(enemyUnit.GetTile()) && enemyUnit.GetComponentsInChildren<TriggerRetaliate>().Length == 0)
                {
                    MenuControl.Instance.battleMenu.AnimationTargetAction(unit.player.GetVisibleBoardCardForCard(unit), enemyUnit.GetTile());
                    return false;
                }
            }
        }

        //Can hero move away?
        bool isHeroSafe = true;
        if (!CheckHeroWontDieAfterMove(player.GetHero(), player.GetHero().GetTile(), null))
        {
            isHeroSafe = false;
        }
        else
        {
            foreach (Tile tile1 in player.GetHero().GetTile().GetAdjacentTilesLinear(1))
            {
                if (tile1.GetUnit() != null && tile1.GetUnit().player != player)
                {
                    isHeroSafe = false;
                    break;
                }
            }
        }
        if (!isHeroSafe && BurnCanMoveCheck(player.GetHero()))
        {
            List<Tile> safeTiles = new List<Tile>();
            foreach (Tile tile in MenuControl.Instance.battleMenu.boardMenu.tiles)
            {
                if (tile.isMoveable())
                {
                    bool safeSpot = true;
                    if (!CheckHeroWontDieAfterMove(player.GetHero(), tile, null))
                    {
                        safeSpot = false;
                    }
                    else
                    {
                        foreach (Tile tile1 in tile.GetAdjacentTilesLinear(1))
                        {
                            if (tile1.GetUnit() != null && tile1.GetUnit().player != player)
                            {
                                safeSpot = false;
                                break;
                            }
                        }
                    }
                    if (safeSpot)
                    {
                        if (player.GetHero().CanTarget(tile))
                        {
                            MenuControl.Instance.battleMenu.AnimationTargetAction(player.GetVisibleBoardCardForCard(player.GetHero()), tile);
                            return false;
                        }
                        else
                        {
                            safeTiles.Add(tile);
                        }
                    }
                }
            }
            if (player.GetHero().remainingMoves > 1 && safeTiles.Count > 0)
            {
                foreach (Tile safeTile in safeTiles)
                {
                    Tile originalTile = player.GetHero().GetTile();
                    //Pick any other random empty tile 
                    foreach (Tile tile in MenuControl.Instance.battleMenu.boardMenu.tiles)
                    {
                        if (tile.isMoveable())
                        {
                            if (player.GetHero().CanTarget(tile))
                            {
                                player.GetHero().MoveToTile(tile);
                                if (player.GetHero().CanTarget(safeTile))
                                {
                                    player.GetHero().MoveToTile(originalTile);
                                    MenuControl.Instance.battleMenu.AnimationTargetAction(player.GetVisibleBoardCardForCard(player.GetHero()), tile);
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            if (player.GetHero().remainingMoves > 0 && safeTiles.Count == 0)
            {
                //Move to a tile with more empty space or friendlies adjacent
                int count = 0;
                foreach (Tile tile in player.GetHero().GetTile().GetAdjacentTilesLinear(1))
                {
                    if (tile.isMoveable() || tile.GetUnit().player == player)
                    {
                        count += 1;
                    }
                }

                //Pick any other random empty tile 
                foreach (Tile tile in MenuControl.Instance.battleMenu.boardMenu.tiles)
                {
                    if (tile.isMoveable())
                    {
                        if (player.GetHero().CanTarget(tile))
                        {
                            int count2 = 0;
                            foreach (Tile tile2 in tile.GetAdjacentTilesLinear(1))
                            {
                                if (tile2.isMoveable() || tile2.GetUnit().player == player)
                                {
                                    count2 += 1;
                                }
                            }
                            if (count2 > count)
                            {
                                MenuControl.Instance.battleMenu.AnimationTargetAction(player.GetVisibleBoardCardForCard(player.GetHero()), tile);
                                return false;
                            }
                        }

                    }
                }
            }
        }

        if (!SummonMeleeMinionsCloseToEnemyHero(player)) return false;

        if (!SummonMinionsFarFromEnemyHero(player)) return false;

        if (!OffensiveCastables(player)) return false;

        if (!BuffingCastablesHero(player)) return false;

        if (!BuffingCastablesMinions(player)) return false;

        if (!UnitsAttackLogic(player))
        {
            return false;
        }

        //Move units closer to enemies
        foreach (Unit unit in player.cardsOnBoard.OrderBy(x => Vector2.Distance(((Unit)x).GetTile().transform.position, player.GetOpponent().GetHero().GetTile().transform.position)))
        {
            if (unit.CanMove() && !(unit is Hero) && BurnShouldMoveCheck(unit))
            {
                List<Tile> validTiles = new List<Tile>();
                foreach (Tile tile in MenuControl.Instance.battleMenu.boardMenu.tiles)
                {
                    if ((tile.isMoveable() || tile.GetUnit() == unit) && tile != unit.GetTile() && unit.CanTarget(tile))
                    {
                        validTiles.Add(tile);
                    }
                }

                //Choose the tile that is closest to enemy hero
                if (validTiles.Count > 0)
                {
                    Tile nextTile = validTiles.OrderBy(x => Vector2.Distance(x.transform.position, player.GetOpponent().GetHero().GetTile().transform.position)).First();
                    if (CheckMaintainPinOnEnemyHero(unit, nextTile))
                    {
                        MenuControl.Instance.battleMenu.AnimationTargetAction(unit.player.GetVisibleBoardCardForCard(unit), nextTile);
                        return false;
                    }
                }
            }
        }

        if (!UnitsAidFriendlyUnits(player)) return false;

        return true;
    }

}
