using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetOtherUnit : Ability
{
    public enum TargetUnitType {
        MyHero, EnemyHero, ThisUnit, RandomFriendlyMinion, RandomOtherFriendlyMinion, RandomOtherMinion, RandomOtherFriendlyAdjacentMinion, RandomOtherAdjacentMinion, SelfIfChargesOne, SelfIfNoFriendlyMinions, SelfIfFullActions, LowestPowerEnemyMinion, SourceUnitOnBoard, SelfIfFullMoves, HeroOfUnitOnTile, RandomAdjacentEnemyMinion, RandomAdjacentEnemy, EnemyUnitsInRange2, FriendlyUnitsInRange2,PlayerHero,AIHero
        ,AllUnits,RandomFriendlyUnit,RandomEnemyUnit,RandomEnemyMinion,RandomOtherFriendlyUnit,RandomUnit
    }

    public Ability otherAbilityToPerform;

    public TargetUnitType targetUnitType;
    public bool useTargeTileUnit = false;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        if (targetUnitType == TargetUnitType.MyHero)
        {
            otherAbilityToPerform.PerformAbility(sourceCard, GetCard().player.GetHero().GetTile(), amount);
            return;
        }
        else if (targetUnitType == TargetUnitType.PlayerHero)
        {
            otherAbilityToPerform.PerformAbility(sourceCard, MenuControl.Instance.battleMenu.player1.GetHero().GetTile(), amount);
            return;
        }
        else if (targetUnitType == TargetUnitType.AIHero)
        {
            otherAbilityToPerform.PerformAbility(sourceCard, MenuControl.Instance.battleMenu.playerAI.GetHero().GetTile(), amount);
            return;
        }

        else if (targetUnitType == TargetUnitType.EnemyHero)
        {
            otherAbilityToPerform.PerformAbility(sourceCard, GetCard().player.GetOpponent().GetHero().GetTile(), amount);
            return;
        }
        else if (targetUnitType == TargetUnitType.ThisUnit)
        {
            if (((Unit)GetCard()).GetZone() == MenuControl.Instance.battleMenu.board)
            {
                otherAbilityToPerform.PerformAbility(sourceCard, ((Unit)GetCard()).GetTile(), amount);
                return;
            }
        }
        else if (targetUnitType == TargetUnitType.RandomFriendlyMinion)
        {
            if (GetCard().player.GetMinionsOnBoard().Count > 0)
            {
               
                Unit unit = GetCard().player.GetMinionsOnBoard()[Random.Range(0, GetCard().player.GetMinionsOnBoard().Count)];
                otherAbilityToPerform.PerformAbility(sourceCard, unit.GetTile(), amount);

                return;
            }
        }
        else if (targetUnitType == TargetUnitType.RandomFriendlyUnit)
        {
            if (GetCard().player.cardsOnBoard.Count > 0)
            {
               
                Unit unit = GetCard().player.cardsOnBoard.RandomItem() as Unit;
                otherAbilityToPerform.PerformAbility(sourceCard, unit.GetTile(), amount);

                return;
            }
        }
        else if (targetUnitType == TargetUnitType.RandomEnemyUnit)
        {
            if (GetCard().player.GetOpponent().cardsOnBoard.Count > 0)
            {
               
                Unit unit = GetCard().player.GetOpponent().cardsOnBoard.RandomItem() as Unit;
                otherAbilityToPerform.PerformAbility(sourceCard, unit.GetTile(), amount);

                return;
            }
        }
        else if (targetUnitType == TargetUnitType.RandomEnemyMinion)
        {
            if (GetCard().player.GetOpponent().GetMinionsOnBoard().Count > 0)
            {
               
                Unit unit = GetCard().player.GetOpponent().GetMinionsOnBoard()[Random.Range(0, GetCard().player.GetOpponent().GetMinionsOnBoard().Count)];
                otherAbilityToPerform.PerformAbility(sourceCard, unit.GetTile(), amount);

                return;
            }
        }
        else if (targetUnitType == TargetUnitType.RandomUnit)
        {
            if (MenuControl.Instance.battleMenu.GetAllUnitsOnBoard().Count > 0)
            {
               
                Unit unit = MenuControl.Instance.battleMenu.GetAllUnitsOnBoard().RandomItem();
                otherAbilityToPerform.PerformAbility(sourceCard, unit.GetTile(), amount);

                return;
            }
        }
        else if (targetUnitType == TargetUnitType.RandomOtherFriendlyUnit)
        {
            List<Card> otherMinions = GetCard().player.cardsOnBoard;
            if (sourceCard is Minion && otherMinions.Contains((Minion)sourceCard))
            {
                otherMinions.Remove((Minion)sourceCard);
            }

            if (otherMinions.Count > 0)
            {
                Unit unit = otherMinions[Random.Range(0, otherMinions.Count)] as Unit;
                if (unit)
                {
                    otherAbilityToPerform.PerformAbility(sourceCard, unit.GetTile(), amount);
                }
               
                return;
            }
        }
        else if (targetUnitType == TargetUnitType.RandomFriendlyMinion)
        {
            if (GetCard().player.GetMinionsOnBoard().Count > 0)
            {
               
                Unit unit = GetCard().player.GetMinionsOnBoard()[Random.Range(0, GetCard().player.GetMinionsOnBoard().Count)];
                otherAbilityToPerform.PerformAbility(sourceCard, unit.GetTile(), amount);

                return;
            }
        }
        else if (targetUnitType == TargetUnitType.RandomOtherFriendlyMinion)
        {
            List<Minion> otherMinions = GetCard().player.GetMinionsOnBoard();
            if (sourceCard is Minion && otherMinions.Contains((Minion)sourceCard))
            {
                otherMinions.Remove((Minion)sourceCard);
            }

            if (otherMinions.Count > 0)
            {
                Unit unit = otherMinions[Random.Range(0, otherMinions.Count)];
                otherAbilityToPerform.PerformAbility(sourceCard, unit.GetTile(), amount);
               
                return;
            }
        }
        else if (targetUnitType == TargetUnitType.RandomOtherMinion)
        {
            List<Minion> otherMinions = GetCard().player.GetMinionsOnBoard();
            otherMinions.AddRange(GetCard().player.GetOpponent().GetMinionsOnBoard());
            if (sourceCard is Minion && otherMinions.Contains((Minion)sourceCard))
            {
                otherMinions.Remove((Minion)sourceCard);
            }

            if (otherMinions.Count > 0)
            {
                Unit unit = otherMinions[Random.Range(0, otherMinions.Count)];
                otherAbilityToPerform.PerformAbility(sourceCard, unit.GetTile(), amount);
         
                return;
            }
        }
        else if (targetUnitType == TargetUnitType.RandomOtherFriendlyAdjacentMinion)
        {
            List<Minion> otherMinions = new List<Minion>();
            foreach (Tile tile in ((Unit)GetCard()).GetTile().GetAdjacentTilesLinear())
            {
                if (tile.GetUnit() != null && tile.GetUnit().player == GetCard().player && tile.GetUnit() is Minion)
                {
                    otherMinions.Add((Minion)tile.GetUnit());
                }
            }

            if (otherMinions.Count > 0)
            {
                Unit unit = otherMinions[Random.Range(0, otherMinions.Count)];
                otherAbilityToPerform.PerformAbility(sourceCard, unit.GetTile(), amount);

                return;
            }
        }
        else if (targetUnitType == TargetUnitType.RandomOtherAdjacentMinion)
        {
            List<Minion> otherMinions = new List<Minion>();
            foreach (Tile tile in ((Unit)GetCard()).GetTile().GetAdjacentTilesLinear())
            {
                if (tile.GetUnit() != null && tile.GetUnit() is Minion)
                {
                    otherMinions.Add((Minion)tile.GetUnit());
                }
            }

            if (otherMinions.Count > 0)
            {
                Unit unit = otherMinions[Random.Range(0, otherMinions.Count)];
                otherAbilityToPerform.PerformAbility(sourceCard, unit.GetTile(), amount);

                return;
            }
        }
        else if (targetUnitType == TargetUnitType.SelfIfChargesOne)
        {
            if (GetEffect() != null && GetEffect().remainingCharges == 1)
            {
                otherAbilityToPerform.PerformAbility(sourceCard, ((Unit)GetCard()).GetTile(), amount);
                return;
            }
        }
        else if (targetUnitType == TargetUnitType.SelfIfNoFriendlyMinions)
        {
            if (sourceCard.player.GetMinionsOnBoard().Count == 0)
            {
                otherAbilityToPerform.PerformAbility(sourceCard, ((Unit)GetCard()).GetTile(), amount);
                return;
            }
        }
        else if (targetUnitType == TargetUnitType.SelfIfFullActions)
        {
            if (((Unit)GetCard()).remainingActions >= ((Unit)GetCard()).initialActions)
            {
                otherAbilityToPerform.PerformAbility(sourceCard, ((Unit)GetCard()).GetTile(), amount);
                return;
            }
        }
        else if (targetUnitType == TargetUnitType.LowestPowerEnemyMinion)
        {
            Minion otherMinion = null;
            foreach (Minion minion in sourceCard.player.GetOpponent().GetMinionsOnBoard())
            {
                if (otherMinion == null || minion.GetPower() < otherMinion.GetPower())
                {
                    otherMinion = minion;
                }
            }

            if (otherMinion != null)
            {
                otherAbilityToPerform.PerformAbility(sourceCard, otherMinion.GetTile(), amount);
                return;
            }
        }
        else if (targetUnitType == TargetUnitType.SourceUnitOnBoard)
        {
            if (sourceCard is Unit && ((Unit)sourceCard).GetZone() == MenuControl.Instance.battleMenu.board)
            {
                otherAbilityToPerform.PerformAbility(sourceCard, ((Unit)sourceCard).GetTile(), amount);
                return;
            }
        }
        else if (targetUnitType == TargetUnitType.SelfIfFullMoves)
        {
            //if (((Unit)GetCard()).remainingMoves >= ((Unit)GetCard()).GetInitialMoves())
            if(!((Unit)GetCard()).hasMovedThisTurn)
            {
                otherAbilityToPerform.PerformAbility(sourceCard, ((Unit)GetCard()).GetTile(), amount);
            }
        }
        else if (targetUnitType == TargetUnitType.HeroOfUnitOnTile)
        {
            if (targetTile.GetUnit() != null)
            {
                otherAbilityToPerform.PerformAbility(sourceCard, targetTile.GetUnit().player.GetHero().GetTile(), amount);
                return;
            }
        }
        else if (targetUnitType == TargetUnitType.RandomAdjacentEnemyMinion)
        {
            List<Minion> otherMinions = new List<Minion>();
            foreach (Tile tile in ((Unit)GetCard()).GetTile().GetAdjacentTilesLinear())
            {
                if (tile.GetUnit() != null && tile.GetUnit().player != GetCard().player && tile.GetUnit() is Minion)
                {
                    otherMinions.Add((Minion)tile.GetUnit());
                }
            }

            if (otherMinions.Count > 0)
            {
                Unit unit = otherMinions[Random.Range(0, otherMinions.Count)];
                otherAbilityToPerform.PerformAbility(sourceCard, unit.GetTile(), amount);

                return;
            }
        }
        else if (targetUnitType == TargetUnitType.RandomAdjacentEnemy)
        {
            List<Unit> otherMinions = new List<Unit>();
            Tile tempTargetTile = null;
            //考虑到目标直接被杀死的情况
            if (useTargeTileUnit )
            {
                if (targetTile)
                {
                    tempTargetTile = targetTile;
                }
                else
                {
                    return;
                }
            }
            else
            {
                
                var cardUnit = GetCard() as Unit;
                if (!cardUnit)
                {
                    cardUnit = GetCard().player.GetHero();
                }
                if (cardUnit == null)
                {
                    return;
                }

                tempTargetTile = cardUnit.GetTile();
            }

            foreach (Tile tile in tempTargetTile.GetAdjacentTilesLinear())
            {
                if (tile.GetUnit() != null && tile.GetUnit().player != GetCard().player)
                {
                    if (!otherMinions.Contains(tile.GetUnit()))
                        otherMinions.Add(tile.GetUnit());
                }
            }

            if (otherMinions.Count > 0)
            {
                Unit unit = otherMinions[Random.Range(0, otherMinions.Count)];
                otherAbilityToPerform.PerformAbility(sourceCard, unit.GetTile(), amount);

                return;
            }
        }
        else if (targetUnitType == TargetUnitType.EnemyUnitsInRange2)
        {

            List<Unit> otherUnits = new List<Unit>();
            if (targetTile.GetUnit() is LargeHero)
            {
                foreach (Tile tile2 in ((LargeHero)targetTile.GetUnit()).GetTiles())
                {
                    foreach (Tile tile in tile2.GetAdjacentTilesLinear(2))
                    {
                        if (tile.GetUnit() != null && tile.GetUnit().player != GetCard().player)
                        {
                            if (!otherUnits.Contains(tile.GetUnit()))
                                otherUnits.Add(tile.GetUnit());
                        }
                    }
                }
            }
            else
            {
                foreach (Tile tile in targetTile.GetAdjacentTilesLinear(2))
                {
                    if (tile.GetUnit() != null && tile.GetUnit().player != GetCard().player)
                    {
                        if (!otherUnits.Contains(tile.GetUnit()))
                            otherUnits.Add(tile.GetUnit());
                    }
                }
            }

            foreach (Unit unit in otherUnits)
            {
                otherAbilityToPerform.PerformAbility(sourceCard, unit.GetTile(), amount);

            }

        }
        else if (targetUnitType == TargetUnitType.FriendlyUnitsInRange2)
        {

            List<Unit> otherUnits = new List<Unit>();
            if (targetTile.GetUnit() is LargeHero)
            {
                foreach (Tile tile2 in ((LargeHero)targetTile.GetUnit()).GetTiles())
                {
                    foreach (Tile tile in tile2.GetAdjacentTilesLinear(2))
                    {
                        if (tile.GetUnit() != null && tile.GetUnit().player == GetCard().player)
                        {
                            if (!otherUnits.Contains(tile.GetUnit()))
                                otherUnits.Add(tile.GetUnit());
                        }
                    }
                }
            }
            else
            {
                foreach (Tile tile in targetTile.GetAdjacentTilesLinear(2))
                {
                    if (tile.GetUnit() != null && tile.GetUnit().player == GetCard().player)
                    {
                        if (!otherUnits.Contains(tile.GetUnit()))
                            otherUnits.Add(tile.GetUnit());
                    }
                }
            }

            foreach (Unit unit in otherUnits)
            {
                otherAbilityToPerform.PerformAbility(sourceCard, unit.GetTile(), amount);

            }

        }else if (targetUnitType == TargetUnitType.AllUnits)
        {
            foreach (var unit in MenuControl.Instance.battleMenu.GetAllUnitsOnBoard())
            {
                otherAbilityToPerform.PerformAbility(sourceCard, unit.GetTile(), amount);
            }
        }


    }
}
