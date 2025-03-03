using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureItemKnownEvent : AdventureItem {

    public EventDefinition eventDefinition;

    public override void PerformItem(int index)
    {
        MenuControl.Instance.eventMenu.ShowEvent(eventDefinition);
    }
}
