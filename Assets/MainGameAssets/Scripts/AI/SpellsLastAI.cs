using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "SpellsLastAI", menuName = "Game Data/AI/SpellsLastAI", order = 1)]
public class SpellsLastAI : AIControl
{
    public override bool TakeAITurn(Player player)
    {

        if (!UnitsAttackLogic(player))
        {
            return false;
        }

        if (!UnitsMoveCloserToEnemyHero(player)) return false;

        if (!SummonMeleeMinionsCloseToEnemyHero(player)) return false;

        if (!SummonMinionsFarFromEnemyHero(player)) return false;

        if (!OffensiveCastables(player)) return false;

        if (!BuffingCastablesHero(player)) return false;

        if (!BuffingCastablesMinions(player)) return false;

        return true;
    }


}
