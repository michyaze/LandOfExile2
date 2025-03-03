using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoaringMeteors : Ability
{
    public Effect meteorEffectTemplate;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        List<Minion> enemyMinions = GetCard().player.GetOpponent().GetMinionsOnBoard();
        enemyMinions.Shuffle();

        for (int ii = 0; ii < 4; ii += 1)
        {
            if (enemyMinions.Count > ii)
            {
                Minion minion = enemyMinions[ii];
                minion.ApplyEffect(GetCard(), this, meteorEffectTemplate, 1);
            }
        }
    }
}
