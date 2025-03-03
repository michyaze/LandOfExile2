using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceGoBack : EventChoice
{
    public bool shouldRemoveEvent = false;
    public override void PerformChoice()
    {
        MenuControl.Instance.eventMenu.CloseMenu();
        if (shouldRemoveEvent)
        {
            MenuControl.Instance.adventureMenu.RemoveItem();
        }
    }

}
