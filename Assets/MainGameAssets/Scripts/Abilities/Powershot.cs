using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powershot : Ability
{
    public int intialDamage = 6;
    public int amountLostPerUnit = 2;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        Tile firstTile = sourceCard.player.GetHero().GetTile();
        Tile lastTile = sourceCard.player.GetOpponent().GetHero().GetTile();

        Vector3 direction = lastTile.transform.position - firstTile.transform.position;
        List<Unit> otherUnitsHit = new List<Unit>();
        foreach (Tile tile in MenuControl.Instance.battleMenu.boardMenu.tiles)
        {

            Vector3 startingPoint = firstTile.transform.position;
            Ray ray = new Ray(startingPoint, direction);
            float distance = Vector3.Cross(ray.direction, tile.transform.position - ray.origin).magnitude;

            float tileWidth = 1.25f;
            if (distance < tileWidth / 2f && Vector2.Distance(firstTile.transform.position, tile.transform.position) < Vector2.Distance(firstTile.transform.position, lastTile.transform.position))
            {
                if (tile.GetUnit() != null && tile.GetUnit() != sourceCard.player.GetHero() && tile.GetUnit() != sourceCard.player.GetOpponent().GetHero())
                {
                    if (!otherUnitsHit.Contains(tile.GetUnit()))
                    {
                        otherUnitsHit.Add(tile.GetUnit());
                    }
                }
            }
        }

        int finalDamage = intialDamage - (otherUnitsHit.Count * amountLostPerUnit);

        foreach (Unit unit in otherUnitsHit)
        {
            unit.SufferDamage(sourceCard, this, finalDamage);
        }

        sourceCard.player.GetOpponent().GetHero().SufferDamage(sourceCard, this, finalDamage);
    }
}
