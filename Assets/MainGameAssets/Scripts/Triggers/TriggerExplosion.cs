using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerExplosion : Trigger
{
    public int damageAmount = 10;

    public override void MinionDestroyed(Card sourceCard, Ability ability, int damageAmount, Minion minion)
    {
        if (GetCard() == minion)
        {
            QueueUpExplosion();
        }
    }

    void QueueUpExplosion()
    {
        Tile targetTile = ((Unit)GetCard()).GetTile();
        Card sourceCard = GetCard();
        sourceCard.PutIntoZone(MenuControl.Instance.battleMenu.discard);

        MenuControl.Instance.battleMenu.AddTriggeredAbility(sourceCard, GetEffect(), () =>
        {

            CardTag cardTag = null;
            bool inSameRow = true;
            bool inSameColumn = true;
            bool enemyUnits = true;
            bool friendlyUnits = true;
            bool enemyMinions = false;
            bool friendlyMinions = false;



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
                            if (cardTag == null || unit.cardTags.Contains(cardTag))
                            {
                                if ((enemyUnits && unit.player == sourceCard.player.GetOpponent()) || (friendlyUnits && unit.player == sourceCard.player) || (enemyMinions && unit.player == sourceCard.player.GetOpponent() && unit is Minion) || (friendlyMinions && unit.player == sourceCard.player && unit is Minion))
                                {
                                    unit.SufferDamage(sourceCard, this, damageAmount);
                                }


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
                            if (cardTag == null || unit.cardTags.Contains(cardTag))
                            {
                                if ((enemyUnits && unit.player == sourceCard.player.GetOpponent()) || (friendlyUnits && unit.player == sourceCard.player) || (enemyMinions && unit.player == sourceCard.player.GetOpponent() && unit is Minion) || (friendlyMinions && unit.player == sourceCard.player && unit is Minion))
                                {
                                    unit.SufferDamage(sourceCard, this, damageAmount);
                                }


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
                            if (cardTag == null || unit.cardTags.Contains(cardTag))
                            {
                                if ((enemyUnits && unit.player == sourceCard.player.GetOpponent()) || (friendlyUnits && unit.player == sourceCard.player) || (enemyMinions && unit.player == sourceCard.player.GetOpponent() && unit is Minion) || (friendlyMinions && unit.player == sourceCard.player && unit is Minion))
                                {
                                    unit.SufferDamage(sourceCard, this, damageAmount);
                                }


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
                            if (cardTag == null || unit.cardTags.Contains(cardTag))
                            {
                                if ((enemyUnits && unit.player == sourceCard.player.GetOpponent()) || (friendlyUnits && unit.player == sourceCard.player) || (enemyMinions && unit.player == sourceCard.player.GetOpponent() && unit is Minion) || (friendlyMinions && unit.player == sourceCard.player && unit is Minion))
                                {
                                    unit.SufferDamage(sourceCard, this, damageAmount);
                                }


                            }
                        }
                    }

                }
            }

        });
    }

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {


        QueueUpExplosion();
    }
}
