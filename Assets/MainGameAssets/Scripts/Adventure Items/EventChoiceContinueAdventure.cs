using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceContinueAdventure : EventChoice
{
    public override void PerformChoice()
    {
        MenuControl.Instance.eventMenu.CloseMenu();
        MenuControl.Instance.adventureMenu.ContinueAdventure();

    }

}
