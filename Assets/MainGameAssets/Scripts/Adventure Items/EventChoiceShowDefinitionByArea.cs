using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceShowDefinitionByArea : EventChoice
{
    public List<EventDefinition> definitions = new List<EventDefinition>();

    public override void PerformChoice()
    {

        MenuControl.Instance.eventMenu.ShowEvent(definitions[MenuControl.Instance.areaMenu.areasVisited -1]);
    }
}
