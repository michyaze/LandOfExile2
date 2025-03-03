using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformMultiAbility : Ability
{
    public bool checkCanTargetTile = false;
    public List<Ability> abilities;
    public override bool CanTargetTile(Card card, Tile tile)
    {
        if (checkCanTargetTile && abilities.Count>0)
        {
            return abilities[0].CanTargetTile(card,tile);
        }
        return base.CanTargetTile(card, tile);
    }

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        foreach (var ability in abilities)
        {
            ability.PerformAbility(sourceCard, targetTile, amount);
        }
    }
}
