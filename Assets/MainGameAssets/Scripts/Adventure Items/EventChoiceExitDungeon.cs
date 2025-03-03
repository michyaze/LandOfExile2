using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceExitDungeon : EventChoice
{

    public override void PerformChoice()
    {
        MenuControl.Instance.eventMenu.CloseMenu();

        MenuControl.Instance.adventureMenu.EndRun(true);

    }

}
