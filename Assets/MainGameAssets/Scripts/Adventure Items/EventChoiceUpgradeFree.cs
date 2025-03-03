using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceUpgradeFree : EventChoice
{
    public int changeHP;

    public override void PerformChoice()
    {
        MenuControl.Instance.shopMenu.ShowShopFreeUpgrade(GetName(),()=> {

            if (changeHP > 0)
            {
                MenuControl.Instance.heroMenu.hero.Heal(null, null, changeHP);
            }
            else if (changeHP < 0)
            {
                MenuControl.Instance.heroMenu.hero.currentHP += changeHP;
            }

            CloseEvent();
            MenuControl.Instance.adventureMenu.ContinueAdventure();
        });

    }
}
