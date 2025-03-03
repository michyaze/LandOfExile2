using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "OffensiveSpellsFirstAI", menuName = "Game Data/AI/OffensiveSpellsFirstAI", order = 1)]
public class OffensiveSpellsFirstAI : AIControl
{
    public override bool TakeAITurn(Player player)
    {
        if (!OffensiveCastables(player)) return false;

        if (!BuffingCastablesHero(player)) return false;

        if (!BuffingCastablesMinions(player)) return false;

        if (!UnitsAttackLogic(player))
        {
            return false;
        }

        if (!UnitsMoveCloserToEnemyHero(player)) return false;

        if (!SummonMeleeMinionsCloseToEnemyHero(player)) return false;

        if (!SummonMinionsFarFromEnemyHero(player)) return false;

        return true;
    }


}
