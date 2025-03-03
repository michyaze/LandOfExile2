using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceUpgrade : EventChoice
{

    public override void PerformChoice()
    {
        MenuControl.Instance.eventMenu.CloseMenu();
        MenuControl.Instance.shopMenu.ShowShopUpgrade(GetName());

    }
}
