using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceTreasure : EventChoice
{
    public CardTag lootTag;
    public override void PerformChoice()
    {

        MenuControl.Instance.eventMenu.CloseMenu();

        MenuControl.Instance.shopMenu.ShowShopTreasure(MenuControl.Instance.adventureMenu.GetItemCards(), GetName());
    }

}
