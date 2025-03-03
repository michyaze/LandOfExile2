using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ChangeWeapon : Ability
{
    [FormerlySerializedAs("weaponTemplate")] public NewWeapon weaponTemplate;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        sourceCard.player.GetHero().ChangeWeapon(weaponTemplate); 
    }
}
