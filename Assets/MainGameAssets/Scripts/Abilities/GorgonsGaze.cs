using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorgonsGaze : Ability
{
    public Effect stunEffectTemplate;
    public Effect blockEffectTemplate;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        Tile firstTile = sourceCard.player.GetHero().GetTile();

        foreach (Unit unit in sourceCard.player.GetOpponent().cardsOnBoard)
        {
            Tile lastTile = unit.GetTile();

            Vector3 direction = lastTile.transform.position - firstTile.transform.position;
            List<Unit> otherUnitsHit = new List<Unit>();
            foreach (Tile tile in MenuControl.Instance.battleMenu.boardMenu.tiles)
            {
                Unit unitOnTile = tile.GetUnit();
                Vector3 startingPoint = firstTile.transform.position;
                Ray ray = new Ray(startingPoint, direction);
                float distance = Vector3.Cross(ray.direction, tile.transform.position - ray.origin).magnitude;

                float tileWidth = 1.25f;
                if (distance < tileWidth / 2f && Vector2.Distance(firstTile.transform.position, tile.transform.position) < Vector2.Distance(firstTile.transform.position, lastTile.transform.position))
                {
                    if (tile != lastTile && tile != firstTile)
                    {
                        if (unitOnTile != null && unitOnTile != sourceCard.player.GetHero() && unitOnTile != unit)
                        {
                            if (!otherUnitsHit.Contains(tile.GetUnit()) && unitOnTile.player == unit.player)
                            {
                                otherUnitsHit.Add(tile.GetUnit());
                            }
                        }
                    }
                }
            }

            if (!(otherUnitsHit.Count > 0 || unit.GetEffectsWithTemplate(blockEffectTemplate).Count > 0))
            {
                unit.ApplyEffect(sourceCard, this, stunEffectTemplate, 1);
            }


        }


    }
}
