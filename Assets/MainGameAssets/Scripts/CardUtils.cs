using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardUtils
{
    
    public static bool isOpposingTargetType(Unit unit,Ability ability,OpposingTargetType opposingTargetType)
    {
        if (unit is Hero)
        {
            if (unit.player == ability.GetCard().player)
            {
                if ((opposingTargetType & OpposingTargetType.PlayerHero) != OpposingTargetType.PlayerHero)
                {
                    return false;
                }
            }
            else
            {
                if ((opposingTargetType & OpposingTargetType.EnemyBoss) != OpposingTargetType.EnemyBoss)
                {
                    return false;

                }
            }
        }
        else
        {
            if (unit.player == ability.GetCard().player)
            {
                if ((opposingTargetType & OpposingTargetType.PlayerMinion) != OpposingTargetType.PlayerMinion)
                {
                    return false;

                }
            }
            else
            {
                if ((opposingTargetType & OpposingTargetType.EnemyMinion) != OpposingTargetType.EnemyMinion)
                {
                    return false;

                }
            }
        }

        return true;
    }
    public static bool isOpposingTargetType(Unit unit,Card card,OpposingTargetType opposingTargetType)
    {
        if (unit is Hero)
        {
            if (unit.player == card.player)
            {
                if ((opposingTargetType & OpposingTargetType.PlayerHero) != OpposingTargetType.PlayerHero)
                {
                    return false;
                }
            }
            else
            {
                if ((opposingTargetType & OpposingTargetType.EnemyBoss) != OpposingTargetType.EnemyBoss)
                {
                    return false;

                }
            }
        }
        else
        {
            if (unit.player == card.player)
            {
                if ((opposingTargetType & OpposingTargetType.PlayerMinion) != OpposingTargetType.PlayerMinion)
                {
                    return false;

                }
            }
            else
            {
                if ((opposingTargetType & OpposingTargetType.EnemyMinion) != OpposingTargetType.EnemyMinion)
                {
                    return false;

                }
            }
        }

        return true;
    }
}
