using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoicePurchase : EventChoice
{

    public override void PerformChoice()
    {
        MenuControl.Instance.eventMenu.CloseMenu();   
        MenuControl.Instance.shopMenu.ShowShopPurchase(MenuControl.Instance.adventureMenu.GetItemCards(), GetName());

    }

}
