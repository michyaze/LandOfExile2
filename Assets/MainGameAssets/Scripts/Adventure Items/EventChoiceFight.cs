using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceFight : EventChoice
{
    public override void PerformChoice()
    {
        MenuControl.Instance.eventMenu.CloseMenu();
        MenuControl.Instance.adventureMenu.tutorialEncounterOnly = false;
        MenuControl.Instance.adventureMenu.StartEncounter();

    }

}
