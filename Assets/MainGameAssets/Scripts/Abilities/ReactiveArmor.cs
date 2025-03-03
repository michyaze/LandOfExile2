using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveArmor : Ability
{
    public Effect blockEffectTemplate;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        int charges = sourceCard.player.GetOpponent().cardsOnBoard.Count;

        targetTile.GetUnit().ApplyEffect(sourceCard, this, blockEffectTemplate, charges);
    }
}
