using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// this trigger is deprecated
public class TriggerNightmareBlade : Trigger
{
    public override void GameStarted()
    {
        List<NewWeapon> weapons = new List<NewWeapon>();
        foreach (Card card in MenuControl.Instance.heroMenu.allCards)
        {
            if (card is NewWeapon)
            {
                weapons.Add((NewWeapon)card);
            }
        }
        NewWeapon newNewWeapon = weapons[Random.Range(0, weapons.Count)];

        GetCard().player.GetHero().ChangeWeapon(newNewWeapon);
    }
}
