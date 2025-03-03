using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceSetSelection : EventChoice
{
    public int eventId;
    public int selectId;
    public override void PerformChoice()
    {
        MenuControl.Instance.adventureMenu.eventIdSelectOptionId[eventId] = selectId;
        MenuControl.Instance.adventureMenu.RemoveItem();
        MenuControl.Instance.eventMenu.CloseMenu();
    }

}
