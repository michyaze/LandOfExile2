using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneLash : Ability
{
    public int initialDamage = 3;
    public int extraDamage = 0;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        Minion nextUnit = (Minion)targetTile.GetUnit();
        int damageAmount = initialDamage;
        List<Minion> minionsHit = new List<Minion>();
        while (nextUnit != null)
        {
            Tile oldTile = nextUnit.GetTile();
            nextUnit.SufferDamage(GetCard(), this, damageAmount);

            if (nextUnit.GetZone() == MenuControl.Instance.battleMenu.board)
                return;

            minionsHit.Add(nextUnit);
            damageAmount = extraDamage + minionsHit.Count;

            List<Minion> otherMinions = new List<Minion>();
            foreach (Tile tile in oldTile.GetAdjacentTilesLinear(1))
            {
                if (tile.GetUnit() != null && tile.GetUnit() is Minion && tile.GetUnit().player == nextUnit.player && !minionsHit.Contains((Minion)tile.GetUnit()))
                {
                    otherMinions.Add((Minion)tile.GetUnit());
                }
            }

            if (otherMinions.Count > 0)
            {
                nextUnit = otherMinions[Random.Range(0, otherMinions.Count)];
            }
            else
                return;
        }
    }
}
