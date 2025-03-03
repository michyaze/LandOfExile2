using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetOtherUnitsAdjacent : Ability
{
    public Ability otherAbility;
    public CardTag cardTag;
    public bool enemyUnits;
    public bool friendlyUnits;
    public bool enemyMinions;
    public bool friendlyMinions;
    public bool includeDiagonals;
    public bool alsoTargetTile;
    public bool rangeAttackOnly;
    public bool useDifferentValidator = false;

    void performAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (rangeAttackOnly && (targetTile.GetUnit()) && !(targetTile.GetUnit()).isRangedAttack())
        {
            return;
        }

        if (otherAbility)
        {
            
            otherAbility.PerformAbility(sourceCard, targetTile, amount);
        }
        else
        {
            Debug.LogError("no otherAbility for TargetOtherUnitsAdjacent");
        }
    }
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (targetTile == null)
        {
            //Debug.LogError("this target tile is null. which might because otherAbilityToPerform.PerformAbility(sourceCard, unit.GetTile(), amount); and the unit is dead");
            return;
        }
        if (targetTile.GetUnit() != null)
        {
            var adjacentTiles = targetTile.GetUnit().GetAdjacentTiles();
            if (alsoTargetTile)
            {
                performAbility(sourceCard,targetTile,amount);
            }
            foreach (Tile tile in adjacentTiles)
            {
                Unit unit = tile.GetUnit();
                if (unit != null && unit != targetTile.GetUnit())
                {
                    if (!useDifferentValidator && this.GetTargetValidator() &&  unit.GetTile()&& !CanTargetTile(sourceCard, unit.GetTile()))
                    {
                        continue;
                    }
                    if (cardTag == null || unit.cardTags.Contains(cardTag))
                    {
                        if ((enemyUnits && unit.player == GetCard().player.GetOpponent()) ||
                            (friendlyUnits && unit.player == GetCard().player) ||
                            (enemyMinions && unit.player == GetCard().player.GetOpponent() && unit is Minion) ||
                            (friendlyMinions && unit.player == GetCard().player && unit is Minion))
                        {
                            
                            performAbility(sourceCard,unit.GetTile(),amount);
                        }


                    }
                }

            }
        }
        else
        {
            foreach (Tile tile in targetTile.GetAdjacentTilesLinear(1))
            {
                Unit unit = tile.GetUnit();
                if (unit != null && unit != targetTile.GetUnit())
                {
                    if (cardTag == null || unit.cardTags.Contains(cardTag))
                    {
                        if ((enemyUnits && unit.player == GetCard().player.GetOpponent()) ||
                            (friendlyUnits && unit.player == GetCard().player) ||
                            (enemyMinions && unit.player == GetCard().player.GetOpponent() && unit is Minion) ||
                            (friendlyMinions && unit.player == GetCard().player && unit is Minion))
                        {
                            performAbility(sourceCard,unit.GetTile(),amount);
                        }


                    }
                }

            }
        }

        if (includeDiagonals)
        {
            foreach (Tile tile in targetTile.GetUnit().GetDiagonalTiles())
            {
                Unit unit = tile.GetUnit();
                if (unit != null && unit != targetTile.GetUnit())
                {
                    if (cardTag == null || unit.cardTags.Contains(cardTag))
                    {
                        if ((enemyUnits && unit.player == GetCard().player.GetOpponent()) ||
                            (friendlyUnits && unit.player == GetCard().player) ||
                            (enemyMinions && unit.player == GetCard().player.GetOpponent() && unit is Minion) ||
                            (friendlyMinions && unit.player == GetCard().player && unit is Minion))
                        {
                            performAbility(sourceCard,unit.GetTile(),amount);
                        }

                    }
                }
            }
        }

    }

}
