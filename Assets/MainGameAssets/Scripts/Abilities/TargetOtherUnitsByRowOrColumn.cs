using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetOtherUnitsByRowOrColumn : Ability
{
    public Ability otherAbility;
    public CardTag cardTag;
    public bool inSameRow;
    public bool inSameColumn;
    public bool enemyUnits;
    public bool friendlyUnits;
    public bool enemyMinions;
    public bool friendlyMinions;
    public bool unitsAbove0HPOnly;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        if (inSameRow)
        {
            for (int ii = 1; ii <= (MenuControl.Instance.battleMenu.boardMenu.totalActiveTiles / 4) - 1; ii += 1)
            {
                Tile tile = targetTile.GetTileLeft(ii);
                if (tile != null)
                {
                    Unit unit = tile.GetUnit();
                    if (unit != null)
                    {
                        if ((cardTag == null || unit.cardTags.Contains(cardTag)) &&  (!unitsAbove0HPOnly || unit.GetHP() > 0))
                        {
                            if ((enemyUnits && unit.player == GetCard().player.GetOpponent()) || (friendlyUnits && unit.player == GetCard().player) || (enemyMinions && unit.player == GetCard().player.GetOpponent() && unit is Minion) || (friendlyMinions && unit.player == GetCard().player && unit is Minion))
                                otherAbility.PerformAbility(sourceCard, unit.GetTile(), amount);


                        }
                    }
                }

            }

            for (int ii = 1; ii <= (MenuControl.Instance.battleMenu.boardMenu.totalActiveTiles / 4) - 1; ii += 1)
            {
                Tile tile = targetTile.GetTileRight(ii);
                if (tile != null)
                {
                    Unit unit = tile.GetUnit();
                    if (unit != null)
                    {
                        if ((cardTag == null || unit.cardTags.Contains(cardTag)) && (!unitsAbove0HPOnly || unit.GetHP() > 0))
                        {
                            if ((enemyUnits && unit.player == GetCard().player.GetOpponent()) || (friendlyUnits && unit.player == GetCard().player) || (enemyMinions && unit.player == GetCard().player.GetOpponent() && unit is Minion) || (friendlyMinions && unit.player == GetCard().player && unit is Minion))
                                otherAbility.PerformAbility(sourceCard, unit.GetTile(), amount);


                        }
                    }
                }

            }
        }

        if (inSameColumn)
        {
            for (int ii = 1; ii <= 3; ii += 1)
            {
                Tile tile = targetTile.GetTileUp(ii);
                if (tile != null)
                {
                    Unit unit = tile.GetUnit();
                    if (unit != null)
                    {
                        if ((cardTag == null || unit.cardTags.Contains(cardTag)) && (!unitsAbove0HPOnly || unit.GetHP() > 0))
                        {
                            if ((enemyUnits && unit.player == GetCard().player.GetOpponent()) || (friendlyUnits && unit.player == GetCard().player) || (enemyMinions && unit.player == GetCard().player.GetOpponent() && unit is Minion) || (friendlyMinions && unit.player == GetCard().player && unit is Minion))
                                otherAbility.PerformAbility(sourceCard, unit.GetTile(), amount);


                        }
                    }
                }

            }

            for (int ii = 1; ii <= 3; ii += 1)
            {
                Tile tile = targetTile.GetTileDown(ii);
                if (tile != null)
                {
                    Unit unit = tile.GetUnit();
                    if (unit != null)
                    {
                        if ((cardTag == null || unit.cardTags.Contains(cardTag)) && (!unitsAbove0HPOnly || unit.GetHP() > 0))
                        {
                            if ((enemyUnits && unit.player == GetCard().player.GetOpponent()) || (friendlyUnits && unit.player == GetCard().player) || (enemyMinions && unit.player == GetCard().player.GetOpponent() && unit is Minion) || (friendlyMinions && unit.player == GetCard().player && unit is Minion))
                                otherAbility.PerformAbility(sourceCard, unit.GetTile(), amount);


                        }
                    }
                }

            }
        }

    }

}
