using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceShowDefinition : EventChoice
{
    public EventDefinition definition;

    public override void PerformChoice()
    {

        MenuControl.Instance.eventMenu.ShowEvent(definition);
    }
}
