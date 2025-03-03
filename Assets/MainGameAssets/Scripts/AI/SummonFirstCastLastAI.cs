using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "SummonFirstCastLastAI", menuName = "Game Data/AI/SummonFirstCastLastAI", order = 1)]
public class SummonFirstCastLastAI : AIControl
{
    public override bool TakeAITurn(Player player)
    {

        if (!SummonMeleeMinionsCloseToEnemyHero(player)) return false;

        if (!SummonMinionsFarFromEnemyHero(player)) return false;

        if (!UnitsAttackLogic(player))
        {
            return false;
        }

        if (!UnitsMoveCloserToEnemyHero(player)) return false;

        if (!BuffingCastablesMinions(player)) return false;

        if (!BuffingCastablesHero(player)) return false;
        
        if (!UnitsAidFriendlyUnits(player)) return false;

        if (!OffensiveCastables(player)) return false;

        return true;
    }


}
