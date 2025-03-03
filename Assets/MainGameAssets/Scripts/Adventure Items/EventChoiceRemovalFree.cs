using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceRemovalFree : EventChoice
{
    public override void PerformChoice()
    {
        MenuControl.Instance.shopMenu.ShowShopFreeRemoval(GetName(), () =>
        {
            CloseEvent();
        });
    }

}
