using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceHero : Ability
{

    public Hero newHeroTemplate;
    public bool isSummoningSick;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        Hero newHero = GetCard().player.CreateOrReplaceHeroWithTemplate(newHeroTemplate, GetCard().player.GetHero().GetTile());

        if (!isSummoningSick)
        {
            newHero.remainingMoves = newHero.GetInitialMoves();
            newHero.remainingActions = newHero.GetInitialActions();
        }
    }
}
