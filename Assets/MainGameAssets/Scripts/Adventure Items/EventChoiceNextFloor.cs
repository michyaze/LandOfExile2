using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceNextFloor : EventChoice
{

    public override void PerformChoice()
    {
        MenuControl.Instance.areaMenu.currentAreaComplete = true;
        CloseEvent();
        MenuControl.Instance.adventureMenu.ContinueAdventure();

    }
}
