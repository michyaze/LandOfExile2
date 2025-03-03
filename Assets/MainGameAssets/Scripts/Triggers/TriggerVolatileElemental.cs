using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVolatileElemental : Trigger
{
    public int damageAmount = 3;

    public override void MinionDestroyed(Card sourceCard, Ability ability, int damageAmount, Minion minion)
    {
        if (minion == GetCard())
        {
            Unit enemyUnit = null;
            if (ability.GetCard().GetZone() == MenuControl.Instance.battleMenu.board)
            {
                enemyUnit = (Unit)ability.GetCard();
            }
            else if (sourceCard.GetZone() == MenuControl.Instance.battleMenu.board)
            {
                enemyUnit = (Unit)ability.GetCard();
            }

            if (enemyUnit != null)
            {
                Tile thisTile = ((Unit)GetCard()).GetTile();
                for (int ii = 1; ii < 7; ii += 1)
                {
                    if (thisTile.GetTileUp(ii) == enemyUnit.GetTile())
                    {
                        MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                        {
                            for (int xx = 1; xx < 7; xx += 1)
                            {
                                Tile tile = thisTile.GetTileUp(xx);
                                if (tile != null && tile.GetUnit() != null)
                                {

                                    tile.GetUnit().SufferDamage(GetCard(), this, this.damageAmount);

                                }

                            }
                        });
                        return;
                    }
                    if (thisTile.GetTileLeft(ii) == enemyUnit.GetTile())
                    {
                        MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                        {
                            for (int xx = 1; xx < 7; xx += 1)
                            {
                                Tile tile = thisTile.GetTileLeft(xx);
                                if (tile != null && tile.GetUnit() != null)
                                {

                                    tile.GetUnit().SufferDamage(GetCard(), this, this.damageAmount);

                                }

                            }
                        });
                        return;
                    }
                    if (thisTile.GetTileRight(ii) == enemyUnit.GetTile())
                    {
                        MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                        {
                            for (int xx = 1; xx < 7; xx += 1)
                            {
                                Tile tile = thisTile.GetTileRight(xx);
                                if (tile != null && tile.GetUnit() != null)
                                {

                                    tile.GetUnit().SufferDamage(GetCard(), this, this.damageAmount);

                                }

                            }
                        });
                        return;
                    }
                    if (thisTile.GetTileDown(ii) == enemyUnit.GetTile())
                    {
                        MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
                        {
                            for (int xx = 1; xx < 7; xx += 1)
                            {
                                Tile tile = thisTile.GetTileDown(xx);
                                if (tile != null && tile.GetUnit() != null)
                                {

                                    tile.GetUnit().SufferDamage(GetCard(), this, this.damageAmount);

                                }

                            }
                        });
                        return;
                    }
                }

            }
        }
    }
}
