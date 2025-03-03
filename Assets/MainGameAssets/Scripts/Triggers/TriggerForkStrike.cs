using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerForkStrike : Trigger
{
    public override void AfterUnitAttacks(Unit attacker, Unit defender, Tile targetTile, bool initialAttack, bool consumeWeapon)
    {
        if (attacker == GetCard() && initialAttack)
        {
            List<Tile> tilesToHit = new List<Tile>();

            if (attacker is LargeHero)
            {
                foreach (Tile attackerTile in ((LargeHero)attacker).GetTiles())
                {
                    tilesToHit.AddRange(CheckTiles(targetTile, attackerTile));
                }
            }
            else
            {
                tilesToHit.AddRange(CheckTiles(targetTile, attacker.GetTile()));
            }

            if (tilesToHit.Count > 0)
            {

                foreach (Tile tile in tilesToHit)
                {
                    if (tile.GetUnit() != null && tile.GetUnit().player != GetCard().player)
                    {
                        ((Unit)GetCard()).ForceAttack(tile,false,false);
                    }
                }

            }
        }
    }

    List<Tile> CheckTiles(Tile targetTile,  Tile attackerTile)
    {
        List<Tile> tilesToHit = new List<Tile>();
        if (targetTile.GetTileUp() == attackerTile || targetTile.GetTileDown() == attackerTile)
        {
            if (targetTile.GetTileLeft() != null)
                tilesToHit.Add(targetTile.GetTileLeft());
            if (targetTile.GetTileRight() != null)
                tilesToHit.Add(targetTile.GetTileRight());
        }
        else if (targetTile.GetTileRight() == attackerTile || targetTile.GetTileLeft() == attackerTile)
        {
            if (targetTile.GetTileUp() != null)
                tilesToHit.Add(targetTile.GetTileUp());
            if (targetTile.GetTileDown() != null)
                tilesToHit.Add(targetTile.GetTileDown());
        }
       

        return tilesToHit;
    }
}
