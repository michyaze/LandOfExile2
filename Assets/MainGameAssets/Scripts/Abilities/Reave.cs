using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Reave : Ability
{
    public Effect blockTemplate;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        if (GetCard().player.GetOpponent().GetMinionsOnBoard().Count > 0)
        {
            List<Minion> enemyMinions = new List<Minion>();
            enemyMinions.AddRange(GetCard().player.GetOpponent().GetMinionsOnBoard().OrderByDescending(x => x.GetPower()));

            int amountOfBlock = enemyMinions[0].GetPower();

            for (int ii = 0; ii < 3; ii += 1)
            {
                if  (enemyMinions.Count > ii && enemyMinions[ii].GetZone() == MenuControl.Instance.battleMenu.board)
                {
                    enemyMinions[ii].SufferDamage(GetCard(), this, 0, true);
                }
            }

            GetCard().player.GetHero().ApplyEffect(GetCard(), this, blockTemplate, amountOfBlock);
        }

    }
}
