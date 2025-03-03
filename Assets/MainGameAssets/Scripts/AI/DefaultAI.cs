using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "DefaultAI", menuName = "Game Data/AI/DefaultAI", order = 1)]
public class DefaultAI : AIControl
{
    public override bool TakeAITurn(Player player)
    {

        if (!BuffingCastablesHero(player)) return false;

        if (!BuffingCastablesMinions(player)) return false;

        if (!UnitsAttackLogic(player))
        {
            return false;
        }

        if (!UnitsMoveCloserToEnemyHero(player)) return false;

        if (!SummonMeleeMinionsCloseToEnemyHero(player)) return false;

        if (!SummonMinionsFarFromEnemyHero(player)) return false;

        if (!OffensiveCastables(player)) return false;
        
        
        // if (!HeroMoveAndOffensiveCastables(player)) return false;
        //
        // if (!UnitsMoveCloserToEnemyHero(player)) return false;

        if (!UnitsAidFriendlyUnits(player)) return false;


        return true;
    }



}
