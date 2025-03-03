using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetOtherUnitsInRangeFromHero : Ability
{
    public Ability otherAbility;
    public bool enemyUnits;
    public int range = 2;
    public int upToNumber;
    public bool canRepeat = false;
    public TargetMode mode = TargetMode.DirectNumber;
    public int extraAmount = 0;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        Unit thisUnit = sourceCard.player.GetHero();

        List<Tile> tiles = new List<Tile>();
        if ((thisUnit is LargeHero))
        {
            tiles.AddRange(((LargeHero)thisUnit).GetTiles());
        }
        else
        {
            tiles.Add(thisUnit.GetTile());
        }

        List<Tile> tilesToHit = new List<Tile>();

        foreach (Tile tile2 in tiles)
        {

            for (int xx = 1; xx <= range; xx += 1)
            {
                Tile tile = tile2.GetTileLeft(xx);
                if (tile != null && tile.GetUnit() != null && tile.GetUnit() != thisUnit && (!enemyUnits || tile.GetUnit().player != thisUnit.player))
                {
                    tilesToHit.Add(tile);

                }
            }



            for (int xx = 1; xx <= range; xx += 1)
            {
                Tile tile = tile2.GetTileUp(xx);
                if (tile != null && tile.GetUnit() != null && tile.GetUnit() != thisUnit && (!enemyUnits || tile.GetUnit().player != thisUnit.player))
                {
                    tilesToHit.Add(tile);
                }
            }


            for (int xx = 1; xx <= range; xx += 1)
            {
                Tile tile = tile2.GetTileRight(xx);
                if (tile != null && tile.GetUnit() != null && tile.GetUnit() != thisUnit && (!enemyUnits || tile.GetUnit().player != thisUnit.player))
                {
                    tilesToHit.Add(tile);
                }
            }


            for (int xx = 1; xx <= range; xx += 1)
            {
                Tile tile = tile2.GetTileDown(xx);
                if (tile != null && tile.GetUnit() != null && tile.GetUnit() != thisUnit && (!enemyUnits || tile.GetUnit().player != thisUnit.player))
                {
                    tilesToHit.Add(tile); 
                }
            }

        }

        if (mode == TargetMode.PowerOfWeapon)
        {
            amount = sourceCard.player.GetHero().weapon.GetPower() + extraAmount;
        }
        
        if (canRepeat)
        {
            if (tilesToHit.Count != 0)
            {
                
                for (int ii = 0; ii < upToNumber ; ii += 1)
                {
                
                    otherAbility.PerformAbility(sourceCard, tilesToHit[Random.Range(0,tilesToHit.Count)], amount);
                }
            }
        }
        else
        {
            
            if (upToNumber > 0) tilesToHit.Shuffle();
            for (int ii = 0; ii < (upToNumber > 0 ? upToNumber : tilesToHit.Count); ii += 1)
            {
                if (tilesToHit.Count > ii)
                {
                    otherAbility.PerformAbility(sourceCard, tilesToHit[ii], amount);
                }
            }
        }

    }

}
