using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSameConditional : Ability
{
    public enum TargetUnitType {
        NumberOfUnitsOfTypeAreControlled, TileIsEmpty, DamagedMinion, EnemyUnit, FriendlyMinion, LessThan2AdjacentEnemy, FriendlyUnit, Minion
    }
    public int numberToUse;
    public Card otherCardTemplate;
    public Ability otherAbilityToPerform;

    public TargetUnitType targetUnitType;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        if (targetUnitType == TargetUnitType.NumberOfUnitsOfTypeAreControlled)
        {
            int count = 0;
            foreach (Minion minion in sourceCard.player.GetMinionsOnBoard())
            {
                if (minion.cardTemplate.UniqueID == otherCardTemplate.UniqueID)
                {
                    count += 1;
                }
            }
            if (count >= numberToUse)
            {
                otherAbilityToPerform.PerformAbility(sourceCard, targetTile, amount);
                return;
            }

        }
        else if (targetUnitType == TargetUnitType.TileIsEmpty)
        {
            if (targetTile.isMoveable())
            {
                otherAbilityToPerform.PerformAbility(sourceCard, targetTile, amount);
                return;
            }
        }
        else if (targetUnitType == TargetUnitType.DamagedMinion)
        {
            if (targetTile.GetUnit() != null && targetTile.GetUnit() is Minion && targetTile.GetUnit().currentHP < targetTile.GetUnit().GetInitialHP())
            {
                otherAbilityToPerform.PerformAbility(sourceCard, targetTile, amount);
                return;
            }
        }
        else if (targetUnitType == TargetUnitType.EnemyUnit)
        {
            if (targetTile.GetUnit() != null && targetTile.GetUnit().player != GetCard().player)
            {
                otherAbilityToPerform.PerformAbility(sourceCard, targetTile, amount);
                return;
            }
        }
        else if (targetUnitType == TargetUnitType.FriendlyMinion)
        {
            if(targetTile == null || targetTile.GetUnit() == null ||GetCard() ==null)
            {
                //Debug.LogError("TargetSameConditional has some issue "+targetTile+" "+GetCard());
                return;
            }
            if (targetTile.GetUnit() != null && targetTile.GetUnit().player == GetCard().player && targetTile.GetUnit() is Minion)
            {
                otherAbilityToPerform.PerformAbility(sourceCard, targetTile, amount);
                return;
            }
        }
        else if (targetUnitType == TargetUnitType.FriendlyUnit)
        {
            if (targetTile.GetUnit() != null && targetTile.GetUnit().player == GetCard().player)
            {
                otherAbilityToPerform.PerformAbility(sourceCard, targetTile, amount);
                return;
            }
        }
        else if (targetUnitType == TargetUnitType.LessThan2AdjacentEnemy)
        {
            if (targetTile.GetUnit() != null)
            {
                List<Unit> enemies = new List<Unit>();
                foreach (Tile tile in targetTile.GetUnit().GetAdjacentTiles())
                {
                    if (tile.GetUnit() != null && tile.GetUnit().player != GetCard().player)
                    {
                        enemies.Add(tile.GetUnit());
                    }
                }

                if (enemies.Count < 2)
                {
                    otherAbilityToPerform.PerformAbility(sourceCard, targetTile, amount);
                    return;
                }
            }
        }
        else if (targetUnitType == TargetUnitType.Minion)
        {
            if (targetTile.GetUnit() != null && targetTile.GetUnit() is Minion)
            {
                otherAbilityToPerform.PerformAbility(sourceCard, targetTile, amount);
                return;
            }
        }

    }
}
