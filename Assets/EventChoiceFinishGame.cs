using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceFinishGame : EventChoice
{
    public override void PerformChoice()
    {
        MenuControl.Instance.areaMenu.currentAreaComplete = true;
        CloseEvent();
        MenuControl.Instance.adventureMenu.EndRun(false);;

    }
}
