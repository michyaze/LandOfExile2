using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Ability
{

    public List<Ability> otherAbilitiesToPerform = new List<Ability>();

    public bool skipStandardAttack;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        //Perform standard attack
        if (!skipStandardAttack)
            targetTile.GetUnit().SufferDamage(sourceCard, this, ((Unit)sourceCard).GetPower((Unit)sourceCard, targetTile.GetUnit()));

        foreach (Ability ability in otherAbilitiesToPerform)
        {
            if (ability != null)
                ability.PerformAbility(sourceCard, targetTile, amount);
        }
    }

   
}
