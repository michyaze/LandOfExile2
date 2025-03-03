using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetArmlet : Ability
{
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        GetCard().player.GetHero().TargetTile(targetTile, true);
    }
}
