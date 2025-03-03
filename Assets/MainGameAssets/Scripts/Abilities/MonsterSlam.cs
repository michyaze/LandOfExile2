using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSlam : Ability
{
    public override bool CanTargetTile(Card card, Tile tile)
    {
        if (base.CanTargetTile(card, tile))
        {
            return GetTargets(card).Count > 0;
        }

        return false;
    }

    List<Unit> GetTargets(Card sourceCard)
    {
        List<Unit> unitsInRange = new List<Unit>();
        if (sourceCard == null || sourceCard.player == null || !sourceCard.player.GetHero())
        {
            return unitsInRange; //不太对，应该吧intent的卡也设置为player是AI
        }
        Hero hero = sourceCard.player.GetHero();
        int range = 2;

        foreach (Unit unit in sourceCard.player.GetOpponent().cardsOnBoard)
        {
            foreach (Tile tile in hero.GetTiles())
            {
                if (unit.GetTile() && unit.GetTile().GetAdjacentTilesLinear(range).Contains(tile))
                {
                    unitsInRange.Add(unit);
                }
            }
        }

        return unitsInRange;
    }
    
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        Hero hero = (Hero)sourceCard.player.GetHero();
        int range = 2;
        var unitsInRange = GetTargets(sourceCard);

        if (unitsInRange.Count > 0)
        {

            Unit randomUnit = unitsInRange[Random.Range(0, unitsInRange.Count)];
            Tile randomUnitTile = randomUnit.GetTile();

            List<Unit> otherEnemyUnits = new List<Unit>();
            otherEnemyUnits.Add(randomUnit);

            foreach (Tile tile2 in hero.GetTiles())
            {

                for (int ii = 1; ii <= 4; ii += 1)
                {
                    if (randomUnitTile == tile2.GetTileLeft(ii))
                    {
                        for (int xx = 1; xx <= range; xx += 1)
                        {
                            Tile tile = tile2.GetTileLeft(xx);
                            if (tile != null && tile.GetUnit() != null && tile.GetUnit().player != sourceCard.player && tile != randomUnitTile)
                            {
                                if (!otherEnemyUnits.Contains(tile.GetUnit()))
                                    otherEnemyUnits.Add(tile.GetUnit());
                            }
                        }
                        break;

                    }
                    else if (randomUnitTile == tile2.GetTileUp(ii))
                    {
                        for (int xx = 1; xx <= range; xx += 1)
                        {
                            Tile tile = tile2.GetTileUp(xx);
                            if (tile != null && tile.GetUnit() != null && tile.GetUnit().player != sourceCard.player && tile != randomUnitTile)
                            {
                                if (!otherEnemyUnits.Contains(tile.GetUnit()))
                                    otherEnemyUnits.Add(tile.GetUnit());
                            }
                        }
                        break;
                    }
                    else if (randomUnitTile == tile2.GetTileRight(ii))
                    {
                        for (int xx = 1; xx <= range; xx += 1)
                        {
                            Tile tile = tile2.GetTileRight(xx);
                            if (tile != null && tile.GetUnit() != null && tile.GetUnit().player != sourceCard.player && tile != randomUnitTile)
                            {
                                if (!otherEnemyUnits.Contains(tile.GetUnit()))
                                    otherEnemyUnits.Add(tile.GetUnit());
                            }
                        }
                        break;
                    }
                    else if (randomUnitTile == tile2.GetTileDown(ii))
                    {
                        for (int xx = 1; xx <= range; xx += 1)
                        {
                            Tile tile = tile2.GetTileDown(xx);
                            if (tile != null && tile.GetUnit() != null && tile.GetUnit().player != sourceCard.player && tile != randomUnitTile)
                            {
                                if (!otherEnemyUnits.Contains(tile.GetUnit()))
                                    otherEnemyUnits.Add(tile.GetUnit());
                            }
                        }
                        break;
                    }
                }
            }

            foreach (Unit unit in otherEnemyUnits)
            {
                unit.SufferDamage(sourceCard, this, hero.GetPower());
            }
        }

    }
}
