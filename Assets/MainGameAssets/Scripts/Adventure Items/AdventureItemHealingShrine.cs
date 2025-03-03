using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureItemHealingShrine : AdventureItemKnownEvent
{

    public EventDefinition shrineDefinition;

    public override void PerformItem(int index)
    {
        MenuControl.Instance.eventMenu.ShowEvent(shrineDefinition);
    }


}
