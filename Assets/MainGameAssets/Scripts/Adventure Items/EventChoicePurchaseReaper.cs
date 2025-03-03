using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoicePurchaseReaper : EventChoice
{

    public override void PerformChoice()
    {
        MenuControl.Instance.eventMenu.CloseMenu();   
        MenuControl.Instance.shopMenu.ShowShopPurchaseReaper(MenuControl.Instance.adventureMenu.GetItemCards(), GetName());

    }

}
