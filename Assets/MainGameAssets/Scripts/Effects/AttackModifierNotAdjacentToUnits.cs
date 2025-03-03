using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackModifierNotAdjacentToUnits : PowerModifier
{
    public int addition = 3;
    public bool notThisUnit;
    public bool unitsMustBeMissingHealth;
    
    public bool enemyUnits;
    public bool friendlyUnits;
    public bool enemyHero;
    public bool friendlyHero;
    public bool enemyMinions;
    public bool friendlyMinions;
    public override int ModifyPower(Unit unit, int currentPower, Unit attacker, Unit defender)
    {
        bool hasAdjacent = false;
        var targetTile = unit.GetTile();
        Unit thisUnit = (Unit)GetCard();
        if (unit != thisUnit)
        {
            
            return currentPower;
        }
        if (targetTile == null)
        {
            return currentPower;
            
        }
        if (targetTile.GetUnit() != null)
        {
            var adjacentTiles = targetTile.GetUnit().GetAdjacentTiles();
            foreach (Tile tile in adjacentTiles)
            {
                var newUnit = tile.GetUnit();
                if (newUnit != null && newUnit != targetTile.GetUnit())
                {
                    //if (cardTag == null || unit.cardTags.Contains(cardTag))
                    {
                        if ((enemyUnits && newUnit.player == GetCard().player.GetOpponent()) ||
                            (friendlyUnits && newUnit.player == GetCard().player) ||
                            (enemyMinions && newUnit.player == GetCard().player.GetOpponent() && newUnit is Minion) ||
                            (friendlyMinions && newUnit.player == GetCard().player && newUnit is Minion)||
                            (enemyHero && newUnit.player == GetCard().player.GetOpponent() && newUnit is Hero)||
                            (friendlyHero && newUnit.player == GetCard().player && newUnit is Hero)
                            )
                        {
                            hasAdjacent = true;
                            break;
                            //performAbility(sourceCard,unit.GetTile(),amount);
                        }


                    }
                }

            }
        }
        

        if (!hasAdjacent)
        {
            return currentPower+addition;
        }

        return currentPower;
    }


}
